using System.Runtime.Serialization;

namespace BSRBankWCF.Models.WithdrawDepositImpl
{
    /// <summary>
    /// Class representing Withdraw operation, extending <see cref="WithdrawDeposit"/> class
    /// </summary>
    [DataContract]
    public sealed class Withdraw : WithdrawDeposit
    {
        private decimal _amount;

        /// <summary>
        /// Amount of money to deposit. Returns negative stored value.
        /// </summary>
        [DataMember]
        public override decimal Amount
        {
            get { return -_amount; }
            set { _amount = value; }
        }

        /// <summary>
        /// Creates instance of object with all necessary parameters
        /// </summary>
        /// <param name="amount">Amount of money to deposit</param>
        /// <param name="credentials">Base64 encoded credentials</param>
        /// <param name="bankAccountNumber">26-digit Bank account number</param>
        public Withdraw( decimal amount, string credentials, string bankAccountNumber ) : base(credentials, bankAccountNumber)
        {
            Amount = amount;
        }
    }
}
