using System.Runtime.Serialization;
using BSRBankWCF.Models.MessageImpl;

namespace BSRBankWCF.Models
{
    /// <summary>
    /// Base class for <see cref="ErrorMessage"/> and <see cref="ResultMessage"/>
    /// </summary>
    [DataContract]
    [KnownType(typeof(ErrorMessage))]
    [KnownType(typeof(ResultMessage))]
    public abstract class Message
    {
        /// <summary>
        /// Text message
        /// </summary>
        [DataMember]
        public string MessageText { get; set; }

        /// <summary>
        /// Indicates wheather message is describing erorr or success
        /// </summary>
        [DataMember]
        public abstract bool IsError { get; set; }
    }
}