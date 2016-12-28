using MongoDB.Driver;

namespace BSRBankWCF
{
    internal class MongoRepository
    {
        public static IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(Constants.DatabaseUri);
            return client.GetDatabase(Constants.DatabaseName);
        }
    }
}