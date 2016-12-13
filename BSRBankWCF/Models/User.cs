using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BSRBankWCF.Models
{
    [DataContract]
    public class User
    {
        public User(string login, string password)
        {
            Id = getLastIndex() + 1;
            Login = login;
            Password = password;
            Amount = 0;
            BankAccountNumber = AccountUtils.CreateAccountNumber(Constants.BankId, Id);
        }

        [BsonId]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }
        
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string BankAccountNumber { get; set; }

        private int getLastIndex()
        {
            var client = new MongoClient(Constants.DatabaseUri);
            var database = client.GetDatabase(Constants.DatabaseName);

            var user =  database.GetCollection<User>(Constants.UserCollection)
                .Find(new BsonDocument())
                .Sort(new BsonDocument("_id", -1))
                .FirstOrDefault();
            return user?.Id ?? 0;
        }
    }
}
