using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
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
            Accounts.Add(new Account()
            {
                BankAccountNumber = AccountUtils.CreateAccountNumber(Constants.BankId, Id) ,
                Amount = 0
            });
        }

        [BsonId]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public List<Account> Accounts { get; set; } = new List<Account>();

        private int getLastIndex()
        {
            var user = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                .Find(new BsonDocument())
                .Sort(new BsonDocument("_id", -1))
                .FirstOrDefault();
            return user?.Id ?? 0;
        }
    }
}