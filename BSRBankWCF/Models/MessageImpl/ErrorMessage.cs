using System.Runtime.Serialization;

namespace BSRBankWCF.Models.MessageImpl
{
    /// <summary>
    /// Implementation of <see cref="Message"/> class with <see cref="Message.IsError"/> = true.
    /// </summary>
    [DataContract]
    public class ErrorMessage : Message
    { 
        private ErrorMessage()
        {
        }

        /// <summary>
        /// Creates new instance of object with content message
        /// </summary>
        /// <param name="message">Content message</param>
        public ErrorMessage(string message)
        {
            MessageText = message;
        }

        /// <summary>
        /// Indicate wheather operation was succesfull or not. Always true.
        /// </summary>
        [DataMember]
        public override bool IsError
        {
            get { return true; }
            set { }
        }
    }
}