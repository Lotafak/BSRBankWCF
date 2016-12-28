using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    [DataContract]
    internal class Transfer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public string From { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}