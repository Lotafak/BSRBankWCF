using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BSRBankWCF
{
    class MongoRepository
    {
        public static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(Constants.DatabaseUri);
            return client.GetDatabase(Constants.DatabaseName);
        }
    }
}
