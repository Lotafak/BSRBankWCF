using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BSRBankWCF.Models;
using BSRBankWCF.Models.MessageImpl;
using BSRBankWCF.Mongo;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BSRBankWCF.Services.Implementations
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1
    {
        private HttpClient _httpClient;
        public List<Account> GetUserAccounts( string credentials )
        {
            return MongoRepository.GetCollection<User>()
                .Find(Builders<User>.Filter.Eq("Credentials", credentials))
                .FirstOrDefault()?.Accounts;
        }

        public Message AddUser( string login, string password )
        {
            var newUser = new User(login, password);
            User queryUser;

            try
            {
                queryUser = MongoRepository.GetCollection<User>()
                    .Find(Builders<User>.Filter.Eq("Credentials", newUser.Credentials))
                    .FirstOrDefault();
            }
            catch ( MongoExecutionTimeoutException ex )
            {
                return new ErrorMessage($"Przekroczono limit czasu przetwarzania zapytania ! \n{ex.Message}");
            }
            catch ( Exception ex )
            {
                return new ErrorMessage(ex.Message);
            }
            if ( queryUser == null )
                MongoRepository.GetCollection<User>().InsertOneAsync(newUser);
            else
                return new ErrorMessage($"Użytkownik już istenieje !");
            return new ResultMessage($"Utworzono użytkownika. Proszę się zalogować");
        }

        public Message ValidateUser( string login, string password )
        {
            var credentials = AccountUtils.Base64Encode(login + ":" + password);
            var usr = MongoRepository.GetCollection<User>()
                .Find(Builders<User>.Filter.Eq("Credentials", credentials))
                .FirstOrDefault();
            if ( usr == null )
                return new ErrorMessage("Błędny login i/lub hasło");

            return new ResultMessage(credentials);
        }

        /// <summary>
        /// Withdraws or Deposit moeney.
        /// </summary>
        /// <param name="withdrawDeposit"><seealso cref="WithdrawDeposit"/> class store the data needed to Withdraw or Deposit, 
        /// returning negative amount in case of Withdraw</param>
        /// <returns></returns>
        public Message WithdrawDepositMoney( WithdrawDeposit withdrawDeposit )
        {
            var collection = MongoRepository.GetCollection<User>();
            var filter =
                Builders<User>.Filter.Where(
                    x =>
                        x.Credentials == withdrawDeposit.Credentials &&
                        x.Accounts.Any(a => a.BankAccountNumber == withdrawDeposit.BankAccountNumber));

            var user = collection.Find(filter).FirstOrDefault();
            var account = user.Accounts
                .FirstOrDefault(x => x.BankAccountNumber == withdrawDeposit.BankAccountNumber);

            if ( account == null )
                return new ErrorMessage("Nie znaleziono danego konta");

            var newAmount = account.Amount + withdrawDeposit.Amount;

            if ( newAmount < 0 )
                return new ErrorMessage("Saldo nie może być ujemne !");

            var update = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmount);
            var result =
                MongoRepository.GetCollection<User>()
                    .UpdateOne(filter, update);
            if ( result.IsAcknowledged )
            {
                var hist = new History()
                {
                    UserLp = user.Lp,
                    Amount = withdrawDeposit.Amount,
                    From = withdrawDeposit.BankAccountNumber,
                    To = withdrawDeposit.BankAccountNumber,
                    Date = DateTime.Now,
                    Type = Constants.WithdrawDepositType
                };
                var historyCollection = MongoRepository.GetCollection<History>();
                historyCollection.InsertOneAsync(hist);
                return new ResultMessage("Operacja zakończona sukcesem !");
            }

            return new ErrorMessage("Aktualizacja stanu konta zakończona niepowodzeniem.");
        }

        public Message ExecuteExternalTransfer( Transfer transfer, string accountTo, string credentials )
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"http://localhost:8733/Design_Time_Addresses/BSRBankWCF/RestService/accounts/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var collection = MongoRepository.GetCollection<User>();
            var filter = Builders<User>.Filter.Where(
                x =>
                    x.Credentials == credentials &&
                    x.Accounts.Any(a => a.BankAccountNumber == transfer.From));

            var user = collection.Find(filter)
                .FirstOrDefault();
            if (user == null)
                return new ErrorMessage("Brak użytkownika spełniającego kryteria");

            var account = user.Accounts.FirstOrDefault(x => x.BankAccountNumber == transfer.From);
            if (account == null)
                return new ErrorMessage($"Błędne numer rachunku bankowego: {transfer.From}");

            var newAmount = account.Amount - transfer.Amount;
            if (newAmount < 0)
                return new ErrorMessage("Nie wystarczająca ilość środków na koncie");

            var resultMessageTask = SendRequest(transfer, accountTo);
            resultMessageTask.Wait();
            var resultMessage = resultMessageTask.Result;
            if (resultMessage.IsError)
                return resultMessage;

            var update = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmount);
            var result =
                MongoRepository.GetCollection<User>()
                    .UpdateOne(filter, update);

            if (result.IsAcknowledged)
            {
                var historyCollection = MongoRepository.GetCollection<History>();
                var historyRecord = new History
                {
                    Amount = transfer.Amount,
                    Date = DateTime.Now,
                    From = transfer.From,
                    To = accountTo,
                    Type = Constants.ExternalTransferType,
                    UserLp = user.Lp
                };
                historyCollection.InsertOneAsync(historyRecord);
            }
            return resultMessage;
        }

        public List<History> GetUsersHistory(string credentials)
        {
            var user = MongoRepository.GetCollection<User>().Find(Builders<User>.Filter.Where(x =>x.Credentials == credentials))
                .FirstOrDefault();
            return user == null ? null : MongoRepository.GetCollection<History>()
                .Find(Builders<History>.Filter.Where(x => x.UserLp == user.Lp))
                ?.ToList();
        }

        private async Task<Message> SendRequest( Transfer transfer, string accountTo )
        {
            var response = await _httpClient.PostAsync(accountTo, new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8));

            Message responseMessage;

            if ( response.IsSuccessStatusCode )
                responseMessage = new ResultMessage("Operacja zakończona pomyślnie");
            else
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();

                var error = JsonConvert.DeserializeObject<ErrorResponse>(content);
                responseMessage = new ErrorMessage(error.Error);
            }

            return responseMessage;
        }
    }
}