using BSRBankWCF.Models;
using MongoDB.Driver;

namespace BSRBankWCF.Mongo
{
    internal class MongoRepository
    {
        /// <summary>
        /// Gets database with connection strings from <see cref="Constants"/> class
        /// </summary>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(Constants.DatabaseUri);
            return client.GetDatabase(Constants.DatabaseName);
        }

        /// <summary>
        /// Returns MongoDB collection of given <see cref="T"/> type
        /// </summary>
        /// <typeparam name="T">Collection type (Hisotry, User)</typeparam>
        /// <returns>Mongo Collection of given type</returns>
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