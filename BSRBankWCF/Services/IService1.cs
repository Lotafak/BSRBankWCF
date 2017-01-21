using System.Collections.Generic;
using System.ServiceModel;
using BSRBankWCF.Models;

namespace BSRBankWCF.Services
{
    /// <summary>
    /// Interface for communicaiton with client.
    /// </summary>
    [ServiceContract]
    public interface IService1
    {
        /// <summary>
        /// Gets list of users accounts
        /// </summary>
        /// <param name="credentials">Base64 encoded users credentials</param>
        /// <returns></returns>
        [OperationContract]
        List<Account> GetUserAccounts( string credentials);

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">users password</param>
        /// <returns>Message indicating success or error</returns>
        [OperationContract]
        Message AddUser( string login, string password );

        /// <summary>
        /// Perform user validation
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        [OperationContract]
        Message ValidateUser( string login, string password );

        /// <summary>
        /// Withdraws or Deposit moeney.
        /// </summary>
        /// <param name="withdrawDeposit"><seealso cref="WithdrawDeposit"/> class store the data needed to Withdraw or Deposit, 
        /// returning negative amount in case of Withdraw</param>
        /// <returns></returns>
        [OperationContract]
        Message WithdrawDepositMoney(WithdrawDeposit withdrawDeposit);

        /// <summary>
        /// Execute external transfer with use of http POST call
        /// </summary>
        /// <param name="transfer"><see cref="Transfer"/> object</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Base64 endoded users credentials</param>
        /// <returns></returns>
        [OperationContract]
        Message ExecuteExternalTransfer(Transfer transfer, string accountTo, string credentials);

        /// <summary>
        /// Execute internal transfer
        /// </summary>
        /// <param name="transfer"><see cref="Transfer"/> object</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Base64 endoded users credentials</param>
        /// <returns></returns>
        [OperationContract]
        Message ExecuteInternalTransfer( Transfer transfer, string accountTo, string credentials );

        /// <summary>
        /// Gets users operation history
        /// </summary>
        /// <param name="credentials">Base64 encoded credentials</param>
        /// <returns></returns>
        [OperationContract]
        List<History> GetUsersHistory(string credentials);

        /// <summary>
        /// Create new bank account for specified user
        /// </summary>
        /// <param name="credentials">Users </param>
        /// <returns></returns>
        [OperationContract]
        Message CreateBankAccount(string credentials);
    }
}