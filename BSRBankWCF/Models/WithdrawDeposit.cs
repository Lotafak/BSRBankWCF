using System.Runtime.Serialization;
using BSRBankWCF.Models.WithdrawDepositImpl;

namespace BSRBankWCF.Models
{
    /// <summary>
    /// Base class for <see cref="Deposit"/> and <see cref="Withdraw"/> classes
    /// </summary>
    [DataContract]
    [KnownType(typeof(Withdraw))]
    [KnownType(typeof(Deposit))]
    public abstract class WithdrawDeposit
    {
        /// <summary>
        /// Amount of money involved in operation
        /// </summary>
        [DataMember]
        public abstract decimal Amount { get; set; }

        /// <summary>
        /// Base64 encoded credentials
        /// </summary>
        [DataMember]
        public string Credentials { get; set; }

        /// <summary>
        /// 26-digit Bank Account number
        /// </summary>
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
