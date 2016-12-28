using System.Runtime.Serialization;

namespace BSRBankWCF
{
    [DataContract]
    internal class ResultMessage : Message
    {
        public ResultMessage()
        {
        }

        public ResultMessage(string message)
        {
            MessageText = message;
        }

        [DataMember]
        public override bool IsError
        {
            get { return false; }
            set { }
        }
    }
}