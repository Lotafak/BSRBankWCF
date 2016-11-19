using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BSRBankWCF.Models;
using MongoDB.Driver;

namespace BSRBankWCF
{
    public class Service1 : IService1
    {
        public string GetBankAccountNumber( User user )
        {
            var client = new MongoClient(Constants.DatabaseUri);
            var database = client.GetDatabase(Constants.DatabaseName);

            var usr = database.GetCollection<User>(Constants.UserCollection)
                .Find(Builders<User>.Filter.Eq("name", user.Name))
                .FirstOrDefault();
            return usr != null ? usr.BankAccountNumber : "No user in database !";
        }

        public void AddUser( User user )
        {
            if ( user == null ) return;

            var client = new MongoClient(Constants.DatabaseUri);
            var database = client.GetDatabase(Constants.DatabaseName);
            database.GetCollection<User>(Constants.UserCollection).InsertOneAsync(user);
        }
    }
}
