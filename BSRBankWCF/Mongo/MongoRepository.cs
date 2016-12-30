using BSRBankWCF.Models;
using MongoDB.Driver;

namespace BSRBankWCF.Mongo
{
    internal class MongoRepository
    {
        public static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(Constants.DatabaseUri);
            return client.GetDatabase(Constants.DatabaseName);
        }

        public static IMongoCollection<T> GetCollection<T>()
        {
            var name = "";
            if ( typeof(T) == typeof(User) )
                name = Constants.UserCollection;
            else if (typeof(T) == typeof(History))
                name = Constants.HistoryCollection;

            return GetDatabase().GetCollection<T>(name);
        }
    }
}