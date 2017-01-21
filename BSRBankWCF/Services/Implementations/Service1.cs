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
using BSRBankWCF.Utils;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BSRBankWCF.Services.Implementations
{
    /// <summary>
    /// Implementation of interface IService1. Eesponsible for 
    /// connecting and manipulating database, validating data 
    /// and also making external transfer (REST) call
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service1 : IService1
    {
        private HttpClient _httpClient;

        /// <summary>
        /// Gets list of users accounts
        /// </summary>
        /// <param name="credentials">Base64 encoded users credentials</param>
        /// <returns></returns>
        public List<Account> GetUserAccounts( string credentials )
        {
            return MongoRepository.GetCollection<User>()
                .Find(Builders<User>.Filter.Eq("Credentials", credentials))
                .FirstOrDefault()?.Accounts;
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">users password</param>
        /// <returns>Message indicating success or error</returns>
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
                return new ErrorMessage("Użytkownik już istenieje !");
            return new ResultMessage("Utworzono użytkownika. Proszę się zalogować");
        }

        /// <summary>
        /// Perform user validation
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
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
                var hist = new History
                {
                    UserOrdinal = user.Ordinal,
                    Amount = withdrawDeposit.Amount,
                    From = withdrawDeposit.BankAccountNumber,
                    To = withdrawDeposit.BankAccountNumber,
                    Date = DateTime.Now,
                    Type = Constants.WithdrawDepositType,
                    Title = "Wpłata własna"
                };
                var historyCollection = MongoRepository.GetCollection<History>();
                historyCollection.InsertOneAsync(hist);
                return new ResultMessage("Operacja zakończona sukcesem !");
            }

            return new ErrorMessage("Aktualizacja stanu konta zakończona niepowodzeniem.");
        }

        /// <summary>
        /// Execute external transfer with use of http POST call
        /// </summary>
        /// <param name="transfer"><see cref="Transfer"/> object</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Base64 endoded users credentials</param>
        /// <returns></returns>
        public Message ExecuteExternalTransfer( Transfer transfer, string accountTo, string credentials )
        {
            var dict = new Dictionary<string, string>();

            try
            {
                var reader =
                    new StreamReader(
                        File.OpenRead("map.csv"));

                reader.ReadLine();
                while ( !reader.EndOfStream )
                {
                    var line = reader.ReadLine();
                    if ( line == null ) return new ErrorMessage("EndOfFileException");

                    var map = line.Split(',');
                    if ( map[1].Last() != '/' )
                        map[1] += '/';
                    dict.Add(map[0], map[1]);
                }
            }
            catch ( FileNotFoundException )
            {
                return new ErrorMessage("Nie znaleziono pliku \"map.csv\"");
            }

            var bankId = accountTo.Substring(2, 8);

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"{dict[bankId]}accounts/");
            //_httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri("http://localhost:8080/accounts/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AccountUtils.Base64Encode("admin:admin"));
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

            var transferCopy = new Transfer
            {
                Amount = transfer.Amount*100,
                From = transfer.From,
                Title = transfer.Title
            };

            // Send POST request
            var resultMessageTask = SendRequest(transferCopy, accountTo);
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
                    UserOrdinal = user.Ordinal,
                    Title = transfer.Title
                };
                historyCollection.InsertOneAsync(historyRecord);
            }
            return resultMessage;
        }
        /// <summary>
        /// Execute internal transfer
        /// </summary>
        /// <param name="transfer"><see cref="Transfer"/> object</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Base64 endoded users credentials</param>
        /// <returns></returns>
        public Message ExecuteInternalTransfer(Transfer transfer, string accountTo, string credentials)
        {
            var filter = Builders<User>.Filter
                .Where(x => x.Credentials == credentials);
            var collection = MongoRepository.GetCollection<User>();

            var user = collection.Find(filter).FirstOrDefault();

            var from = user?.Accounts.FirstOrDefault(a => a.BankAccountNumber == transfer.From);
            var to = user?.Accounts.FirstOrDefault(a => a.BankAccountNumber == accountTo);

            if (user == null)
                return new ErrorMessage("Brak podanego użytkownika");

            if (from == null)
                return new ErrorMessage($"Błędny numer rachunku bankowego: {transfer.From}");
            
            if(to == null)
                return new ErrorMessage($"Błędny numer rachunku bankowego: {accountTo}");

            var newAmountFrom = from.Amount - transfer.Amount;
            if ( newAmountFrom < 0 )
                return new ErrorMessage("Nie wystarczająca ilość środków na koncie");

            var newAmountTo = to.Amount + transfer.Amount;

            var filterFrom = Builders<User>.Filter
                .Where(x => x.Credentials == credentials && x.Accounts.Any(a => a.BankAccountNumber == transfer.From));
            var filterTo = Builders<User>.Filter
                .Where(x => x.Credentials == credentials && x.Accounts.Any(a => a.BankAccountNumber == accountTo));

            var updateFrom = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmountFrom);
            var resultFrom =
                MongoRepository.GetCollection<User>()
                    .UpdateOne(filterFrom, updateFrom);

            var updateTo = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmountTo);
            var resultTo =
                MongoRepository.GetCollection<User>()
                    .UpdateOne(filterTo, updateTo);

            if (!resultTo.IsAcknowledged || !resultFrom.IsAcknowledged) return new ErrorMessage("Operacja przerwana");
            
            var historyCollection = MongoRepository.GetCollection<History>();
            var historyRecord = new History
            {
                Amount = transfer.Amount,
                Date = DateTime.Now,
                From = transfer.From,
                To = accountTo,
                Type = Constants.InternalTransferType,
                UserOrdinal = user.Ordinal,
                Title = transfer.Title
            };
            historyCollection.InsertOneAsync(historyRecord);

            return new ResultMessage("Operacja zakończona pomyślnie");
        }

        /// <summary>
        /// Gets users operation history
        /// </summary>
        /// <param name="credentials">Base64 encoded credentials</param>
        /// <returns></returns>
        public List<History> GetUsersHistory(string credentials)
        {
            var user = MongoRepository.GetCollection<User>()
                .Find(Builders<User>.Filter.Where(x =>x.Credentials == credentials))
                .FirstOrDefault();
            return user == null ? null : MongoRepository.GetCollection<History>()
                .Find(Builders<History>.Filter.Where(x => x.UserOrdinal == user.Ordinal))
                ?.ToList();
        }

        /// <summary>
        /// Create new bank account for specified user
        /// </summary>
        /// <param name="credentials">Users </param>
        /// <returns></returns>
        public Message CreateBankAccount(string credentials)
        {
            var filter = Builders<User>.Filter.Where(x => x.Credentials == credentials);
            var user = MongoRepository.GetCollection<User>()
                .Find(filter)
                .FirstOrDefault();

            if(user == null)
                return new ErrorMessage("Nie znaleziono danego użytkownika");

            user.CreateNewAccount();

            var update = Builders<User>.Update.Set(x => x.Accounts, user.Accounts);
            var result = MongoRepository.GetCollection<User>()
                .UpdateOne(filter, update);
            if (result.IsAcknowledged)
            {
                return new ResultMessage("Poprawnie dodano konto bankowe");
            }
            return new ErrorMessage("Dodawanie konta bankowego zakończone niepowodzeniem");
        }

        private async Task<Message> SendRequest( Transfer transfer, string accountTo )
        {
            var response = await _httpClient.PostAsync(accountTo, new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json"));

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