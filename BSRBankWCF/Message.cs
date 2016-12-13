using System.Runtime.Serialization;

namespace BSRBankWCF
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
