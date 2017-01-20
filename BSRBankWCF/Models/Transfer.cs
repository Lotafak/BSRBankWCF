using System.ComponentModel;
using System.Runtime.Serialization;
using BSRBankWCF.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    [DataContract]
    public class Transfer
    {
        [DataMember]
        [BsonSerializer(typeof(MongoDbDecimalSerializer))]
        public int Amount { get; set; }

        [DataMember]
        [DisplayName("AccountFrom")]
        public string From { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}