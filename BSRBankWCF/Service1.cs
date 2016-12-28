using System;
using System.Collections.Generic;
using System.Linq;
using BSRBankWCF.Models;
using BSRBankWCF.Models.MessageImpl;
using MongoDB.Driver;

namespace BSRBankWCF
{
    public class Service1 : IService1
    {
        public List<Account> GetBankAccounts(string credentials)
        {
            return MongoRepository.GetDatabase()
                .GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("Login", AccountUtils.LoginFromCredentials(credentials)))
                .FirstOrDefault()?.Accounts;
        }

        public Message AddUser(string login, string password)
        {
            if (login == "" || password == "") return new ErrorMessage("Pola(e) nie wypełnione poprawnie !");

            var user = new User(login, password);
            User usr;

            try
            {
                usr = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                    .Find(Builders<User>.Filter.Eq("Login", login))
                    .FirstOrDefault();
            }
            catch (MongoExecutionTimeoutException ex)
            {
                return new ErrorMessage($"Przekroczono limit czasu przetwarzania zapytania ! \n{ex.Message}");
            }
            catch (Exception ex)
            {
                return new ErrorMessage(ex.Message);
            }
            if (usr == null)
                MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection).InsertOneAsync(user);
            else
                return new ErrorMessage($"Użytkownik o loginie: \"{login}\" już istenieje !");
            return new ResultMessage($"Utworzono użytkownika \"{login}\"\nProszę się zalogować");
        }

        public Message ValidateUser(string login, string password)
        {
            var usr = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("Login", login))
                .FirstOrDefault();
            if (usr == null)
                return new ErrorMessage("Brak podanego użytkownika");
            if (usr.Password != password)
                return new ErrorMessage("Błędne hasło");

            return new ResultMessage("Użytkownik zalogowany poprawnie");
        }
        
        /// <summary>
        /// Withdraws or Deposit moeney.
        /// </summary>
        /// <param name="withdrawDeposit"><seealso cref="WithdrawDeposit"/> class store the data needed to Withdraw or Deposit, 
        /// returning negative amount in case of Withdraw</param>
        /// <returns></returns>
        public Message WithdrawDepositMoney(WithdrawDeposit withdrawDeposit)
        {
            var collection = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection);
            var filter =
                Builders<User>.Filter.Where(
                    x =>
                        x.Login == AccountUtils.LoginFromCredentials(withdrawDeposit.Credentials) &&
                        x.Accounts.Any(a => a.BankAccountNumber == withdrawDeposit.BankAccountNumber));

            var account = collection.Find(filter)
                .FirstOrDefault()
                .Accounts
                .FirstOrDefault(x => x.BankAccountNumber == withdrawDeposit.BankAccountNumber);

            if ( account == null )
                return new ErrorMessage("Nie znaleziono danego konta");

            var newAmount = account.Amount + withdrawDeposit.Amount;

            if (newAmount < 0)
                return new ErrorMessage("Saldo nie może być ujemne !");

            var update = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmount);
            var result =
                MongoRepository.GetDatabase()
                    .GetCollection<User>(Constants.UserCollection)
                    .UpdateOneAsync(filter, update)
                    .Result;
            if ( result.IsAcknowledged )
                return new ResultMessage("Operacja zakończona sukcesem !");

            return new ErrorMessage("Aktualizacja stanu konta zakończona niepowodzeniem.");
        }
    }
}