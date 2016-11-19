using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    [DataContract]
    class Transfer
    {
        [BsonId]
        public ObjectId Id{ get; set; }

        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public string From { get; set; }

        [DataMember]
        public string Title { get; set; }
    }
}
