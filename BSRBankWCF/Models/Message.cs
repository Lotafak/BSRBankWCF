using System.Runtime.Serialization;
using BSRBankWCF.Models.MessageImpl;

namespace BSRBankWCF.Models
{
    [DataContract]
    [KnownType(typeof(ErrorMessage))]
    [KnownType(typeof(ResultMessage))]
    public abstract class Message
    {
        [DataMember]
        public string MessageText { get; set; }

        [DataMember]
        public abstract bool IsError { get; set; }
    }
}