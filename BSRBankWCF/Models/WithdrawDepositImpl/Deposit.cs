using System.Runtime.Serialization;

namespace BSRBankWCF.Models.WithdrawDepositImpl
{
    /// <summary>
    /// Class representing Deposit operation, extending <see cref="WithdrawDeposit"/> class
    /// </summary>
    [DataContract]
    public sealed class Deposit : WithdrawDeposit
    {
        /// <summary>
        /// Amount of money to deposit
        /// </summary>
        [DataMember]
        public override decimal Amount { get; set; }

        /// <summary>
        /// Creates instance of object with all necessary parameters
        /// </summary>
        /// <param name="amount">Amount of money to deposit</param>
        /// <param name="credentials">Base64 encoded credentials</param>
        /// <param name="bankAccountNumber">26-digit Bank account number</param>
        public Deposit( decimal amount, string credentials, string bankAccountNumber ) : base(credentials, bankAccountNumber)
        {
            Amount = amount;
        }
    }
}
