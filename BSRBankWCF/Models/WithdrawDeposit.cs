using System.Runtime.Serialization;
using BSRBankWCF.Models.WithdrawDepositImpl;

namespace BSRBankWCF.Models
{
    [DataContract]
    [KnownType(typeof(Withdraw))]
    [KnownType(typeof(Deposit))]
    public abstract class WithdrawDeposit
    {
        [DataMember]
        public abstract decimal Amount { get; set; }

        [DataMember]
        public string Credentials { get; set; }

        [DataMember]
        public string BankAccountNumber { get; set; }

        protected WithdrawDeposit(string credentials, string bankAccountNumber)
        {
            Credentials = credentials;
            BankAccountNumber = bankAccountNumber;
        }

        protected WithdrawDeposit() { }
    }
}
