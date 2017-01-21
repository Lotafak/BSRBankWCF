using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using BSRBankWCF.Mongo;
using BSRBankWCF.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BSRBankWCF.Models
{
    /// <summary>
    /// User model class
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Contructor creating user with new account contains 0 money
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">Users password</param>
        public User(string login, string password)
        {
            Ordinal = getLastUserIndex() + 1;
            Credentials = AccountUtils.Base64Encode(login + ":" + password);
            CreateNewAccount();
        }

        /// <summary>
        /// Creates new account containing 0 money
        /// </summary>
        public void CreateNewAccount()
        {
            Accounts.Add(new Account
            {
                BankAccountNumber = AccountUtils.CreateAccountNumber(Constants.BankId, getLastAccountIndex().Result + 1),
                Amount = 0
            });
        }

        /// <summary>
        /// Users Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Users ordinal number
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Base64 encoded users credentials
        /// </summary>
        [DataMember]
        public string Credentials { get; set; }

        /// <summary>
        /// List of users accounts
        /// </summary>
        [DataMember]
        public List<Account> Accounts { get; set; } = new List<Account>();

        private int getLastUserIndex()
        {
            var user = MongoRepository.GetDatabase().GetCollection<User>(Constants.UserCollection)
                .Find(new BsonDocument())
                .Sort(new BsonDocument("Ordinal", -1))
                .FirstOrDefault();

            return user?.Ordinal ?? 0;
        }

        private async Task<int> getLastAccountIndex()
        {
            var collection = MongoRepository.GetCollection<User>();
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