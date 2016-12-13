using System;
using System.Runtime.Serialization;

namespace BSRBankWCF
{
    [DataContract]
    class ResultMessage : Message
    {
        public ResultMessage() { }

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
