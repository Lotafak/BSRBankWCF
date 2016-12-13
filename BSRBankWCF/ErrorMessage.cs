using System.Runtime.Serialization;

namespace BSRBankWCF
{
    [DataContract]
    public class ErrorMessage : Message
    {
        public ErrorMessage() { }

        public ErrorMessage(string message)
        {
            MessageText = message;
        }

        [DataMember]
        public override bool IsError
        {
            get { return true; }
            set { }
        }
    }
}
