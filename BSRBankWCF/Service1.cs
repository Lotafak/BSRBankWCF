using BSRBankWCF.Models;
using MongoDB.Driver;

namespace BSRBankWCF
{
    public class Service1 : IService1
    {
        public Message GetBankAccountNumber( User user )
        {
            var client = new MongoClient(Constants.DatabaseUri);
            var database = client.GetDatabase(Constants.DatabaseName);

            var usr = database.GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("name", user.Login))
                .FirstOrDefault();
            return usr != null ? (Message) new ResultMessage(usr.BankAccountNumber) : new ErrorMessage("No user in database !");
        }

        public Message AddUser( string login, string password )
        {
            if ( login == "" || password == "") return new ErrorMessage("Pola(e) nie wypełnione poprawnie !");

            var user = new User(login, password);

            var client = new MongoClient(Constants.DatabaseUri);
            var database = client.GetDatabase(Constants.DatabaseName);
            database.GetCollection<User>(Constants.UserCollection).InsertOneAsync(user);
            return new ResultMessage($"Utworzono użytkownika: {login}");
        }

        public string Hello()
        {
            return "Hello";
        }

        public Message ValidateUser( string login, string password )
        {
            var client = new MongoClient(Constants.DatabaseUri);
            var database = client.GetDatabase(Constants.DatabaseName);
            var usr = database.GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("Login", login))
                .FirstOrDefault();
            if ( usr == null )
                return new ErrorMessage("Brak podanego użytkownika");
            if ( usr.Password != password )
                return new ErrorMessage("Błędne hasło");

            return new ResultMessage("Użytkownik zalogowany poprawnie");
        }
    }
}
