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
        /// <summary>
        /// Operation Id 
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// User ordinal number
        /// </summary>
        public int UserOrdinal { get; set; }

        /// <summary>
        /// Operation type. I.e. "Wpłata/wypłata"
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Operation date
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Source bank account number
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Destination bank account number
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Amount of money sent/withdrawn/deposit/received
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Operation title
        /// </summary>
        public string Title { get; set; }
    }
}
