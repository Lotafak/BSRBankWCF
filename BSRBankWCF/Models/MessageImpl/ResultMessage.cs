using System.Runtime.Serialization;

namespace BSRBankWCF.Models.MessageImpl
{
    /// <summary>
    /// Implementation of <see cref="Message"/> class with <see cref="Message.IsError"/> = false.
    /// </summary>
    [DataContract]
    internal class ResultMessage : Message
    {
        private ResultMessage()
        {
        }

        /// <summary>
        /// Creates new instance of object with content message
        /// </summary>
        /// <param name="message">Content message</param>
        public ResultMessage(string message)
        {
            MessageText = message;
        }

        /// <summary>
        /// Indicate wheather operation was succesfull or not. Always false.
        /// </summary>
        [DataMember]
        public override bool IsError
        {
            get { return false; }
            set { }
        }
    }
}