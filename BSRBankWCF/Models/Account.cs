using System.Runtime.Serialization;
using BSRBankWCF.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    /// <summary>
    /// Account model class 
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>
        /// 26-digit bank account number
        /// </summary>
        [DataMember]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Amount of money assigned to account
        /// </summary>
        [BsonSerializer(typeof(MongoDbDecimalSerializer))]
        [DataMember]
        public decimal Amount { get; set; }
    }
}