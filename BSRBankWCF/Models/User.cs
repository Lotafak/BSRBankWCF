using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using BSRBankWCF.Mongo;
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
            Lp = getLastUserIndex() + 1;
            Credentials = AccountUtils.Base64Encode(login + ":" + password);
            
            Accounts.Add(new Account
            {
                BankAccountNumber = AccountUtils.CreateAccountNumber(Constants.BankId, getLastAccountIndex().Result + 1),
                Amount = 0
            });
        }

        public ObjectId Id { get; set; }

        public int Lp { get; set; }

        [DataMember]
        public string Credentials { get; set; }

        [DataMember]
        public List<Account> Accounts { get; set; } = new List<Account>();

        private int getLastUserIndex()
        {
            //    var user = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
            //        .Find(new BsonDocument())
            //        .Sort(new BsonDocument("Lp", -1))
            //        .FirstOrDefault();


            var user = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                .Find(new BsonDocument())
                .FirstOrDefault();

            return user?.Lp ?? 0;
        }

        private async Task<int> getLastAccountIndex()
        {
            var collection = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection);
            var filter = new BsonDocument();
            var maxIndex = 0;
            using ( var cursor = await collection.FindAsync(filter) )
            {
                while ( await cursor.MoveNextAsync() )
                {
                    var users = cursor.Current;
                    maxIndex = users.Select(document => int.Parse(document.Accounts.Max(x => x.BankAccountNumber.Substring(10)))).Concat(new[] {maxIndex}).Max();
                }
            }

            return maxIndex;
        }
    }
}