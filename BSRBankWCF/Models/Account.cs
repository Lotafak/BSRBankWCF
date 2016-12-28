using System.Runtime.Serialization;
using BSRBankWCF.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public string BankAccountNumber { get; set; }

        [BsonSerializer(typeof(MongoDbDecimalSerializer))]
        [DataMember]
        public decimal Amount { get; set; }
    }
}