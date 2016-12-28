using System;
using System.Collections.Generic;
using BSRBankWCF.Models;
using MongoDB.Driver;

namespace BSRBankWCF
{
    public class Service1 : IService1
    {
        public List<Account> GetBankAccounts(string credentials)
        {
            return MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("name", AccountUtils.LoginFromCredentials(credentials))).First().Accounts;
            // TODO: Exception thrown ("x:x" string). Dunno why. Gotta check this out.
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
    }
}