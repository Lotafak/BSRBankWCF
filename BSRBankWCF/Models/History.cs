using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSRBankWCF.Models
{
    /// <summary>
    /// Operations history model
    /// </summary>
    public class History
    {
        public ObjectId Id { get; set; }

        public int UserLp { get; set; }

        public string Type { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public decimal Amount { get; set; }

        public string Title { get; set; }
    }
}
