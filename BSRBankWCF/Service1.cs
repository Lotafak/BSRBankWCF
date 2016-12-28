using System;
using System.Collections.Generic;
using System.Linq;
using BSRBankWCF.Models;
using MongoDB.Driver;

namespace BSRBankWCF
{
    public class Service1 : IService1
    {
        public List<Account> GetBankAccounts( string credentials )
        {
            return MongoRepository.GetDatabase()
                .GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("Login", AccountUtils.LoginFromCredentials(credentials)))
                .FirstOrDefault()?.Accounts;
        }

        public Message AddUser( string login, string password )
        {
            if ( login == "" || password == "" ) return new ErrorMessage("Pola(e) nie wypełnione poprawnie !");

            var user = new User(login, password);
            User usr;

            try
            {
                usr = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                    .Find(Builders<User>.Filter.Eq("Login", login))
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
            if ( usr == null )
                MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection).InsertOneAsync(user);
            else
                return new ErrorMessage($"Użytkownik o loginie: \"{login}\" już istenieje !");
            return new ResultMessage($"Utworzono użytkownika \"{login}\"\nProszę się zalogować");
        }

        public Message ValidateUser( string login, string password )
        {
            var usr = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("Login", login))
                .FirstOrDefault();
            if ( usr == null )
                return new ErrorMessage("Brak podanego użytkownika");
            if ( usr.Password != password )
                return new ErrorMessage("Błędne hasło");

            return new ResultMessage("Użytkownik zalogowany poprawnie");
        }

        public Message DepositMoney( string credentials, int amount, string accountNumber )
        {
            if ( amount <= 0 )
                return new ErrorMessage("Kwota wpłaty nie może być mniejsza lub równa 0 !");

            var collection = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection);
            var filter =
                Builders<User>.Filter.Where(
                    x =>
                        x.Login == AccountUtils.LoginFromCredentials(credentials) &&
                        x.Accounts.Any(a => a.BankAccountNumber == accountNumber));

            var account = collection.Find(filter)
                .FirstOrDefault()
                .Accounts
                .FirstOrDefault(x => x.BankAccountNumber == accountNumber);

            if ( account == null )
                return new ErrorMessage("Nie znaleziono danego konta");

            var newAmount = account.Amount + amount;

            var update = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmount);
            var result = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection).UpdateOneAsync(filter, update).Result;
            if ( result.IsAcknowledged )
                return new ResultMessage("Operacja zakończona sukcesem !");

            return new ErrorMessage("Aktualizacja stanu konta zakończona niepowodzeniem.");

        }

        public Message WithdrawMoney(string credentials, int amount, string accountNumber)
        {
            if ( amount <= 0 )
                return new ErrorMessage("Kwota wypłaty nie może być mniejsza lub równa 0 !");

            var collection = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection);
            var filter =
                Builders<User>.Filter.Where(
                    x =>
                        x.Login == AccountUtils.LoginFromCredentials(credentials) &&
                        x.Accounts.Any(a => a.BankAccountNumber == accountNumber));

            var account = collection.Find(filter)
                .FirstOrDefault()
                .Accounts
                .FirstOrDefault(x => x.BankAccountNumber == accountNumber);

            if ( account == null )
                return new ErrorMessage("Nie znaleziono danego konta");

            var newAmount = account.Amount - amount;
            if(newAmount < 0 )
                return new ErrorMessage("Nie wsytarczająca ilość pieniędzy na koncie !");

            var update = Builders<User>.Update.Set(x => x.Accounts[-1].Amount, newAmount);
            var result = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection).UpdateOneAsync(filter, update).Result;
            if ( result.IsAcknowledged )
                return new ResultMessage("Operacja zakończona sukcesem !");

            return new ErrorMessage("Aktualizacja stanu konta zakończona niepowodzeniem.");
        }
    }
}