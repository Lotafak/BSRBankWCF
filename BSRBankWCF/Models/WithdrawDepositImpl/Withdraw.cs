using System;
using System.Runtime.Serialization;

namespace BSRBankWCF.Models.WithdrawDepositImpl
{
    [DataContract]
    public sealed class Withdraw : WithdrawDeposit
    {
        private decimal _amount;

        [DataMember]
        public override decimal Amount
        {
            get { return -_amount; }
            set { _amount = value; }
        }

        public Withdraw( decimal amount, string credentials, string bankAccountNumber )
            : base(credentials, bankAccountNumber)
        {
            Amount = amount;
        }
    }
}
