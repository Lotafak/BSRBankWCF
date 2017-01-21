using System.ComponentModel;
using System.Runtime.Serialization;
using BSRBankWCF.Mongo;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    /// <summary>
    /// Model class for transfer operation
    /// </summary>
    [DataContract]
    public class Transfer
    {
        /// <summary>
        /// Amount of money sent
        /// </summary>
        [DataMember]
        [BsonSerializer(typeof(MongoDbDecimalSerializer))]
        public int Amount { get; set; }

        /// <summary>
        /// Source bank account number
        /// </summary>
        [DataMember]
        [DisplayName("AccountFrom")]
        public string From { get; set; }

        /// <summary>
        /// Operation title
        /// </summary>
        [DataMember]
        public string Title { get; set; }
    }
}