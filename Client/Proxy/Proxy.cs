using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client.Proxy
{
    internal class Proxy : ClientBase<IService1>, IService1
    {
        /// <summary>
        /// Gets user accounts through service
        /// </summary>
        /// <param name="credentials">Users credentials enoded in base64</param>
        /// <returns>List of user Accounts</returns>
        public List<Account> GetUserAccounts( string credentials)
        {
            return Channel.GetUserAccounts(credentials);
        }

        /// <summary > 
        /// Async version of <seealso cref="GetUserAccounts"/>
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public Task<List<Account>> GetUserAccountsAsync( string credentials)
        {
            return Channel.GetUserAccountsAsync(credentials);
        }

        /// <summary>
        /// Adding new user to database
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        public Message AddUser(string login, string password)
        {
            return Channel.AddUser(login,password);
        }

        /// <summary>
        /// Async version of <see cref="AddUser"/>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<Message> AddUserAsync( string login, string password )
        {
            return Channel.AddUserAsync(login, password);
        }

        /// <summary>
        /// Checks if user of given credentials exist in database
        /// </summary>
        /// <param name="login">Users login</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        public Message ValidateUser( string login, string password )
        {
            return Channel.ValidateUser(login, password);
        }

        /// <summary>
        /// Async version of <see cref="ValidateUser"/>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<Message> ValidateUserAsync( string login, string password )
        {
            return Channel.ValidateUserAsync(login, password);
        }

        /// <summary>
        /// Execute withdraw action on service
        /// </summary>
        /// <param name="withdrawDeposit"></param>
        /// <returns></returns>
        public Message WithdrawDepositMoney(WithdrawDeposit withdrawDeposit)
        {
            return Channel.WithdrawDepositMoney(withdrawDeposit);
        }

        /// <summary>
        /// Async version of <see cref="WithdrawDepositMoney"/>
        /// </summary>
        /// <param name="withdrawDeposit"></param>
        /// <returns></returns>
        public Task<Message> WithdrawDepositMoneyAsync(WithdrawDeposit withdrawDeposit)
        {
            return Channel.WithdrawDepositMoneyAsync(withdrawDeposit);
        }

        /// <summary>
        /// Order external transfer execute operation. 
        /// </summary>
        /// <param name="transfer">Required data to transfer</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Users credentials encoded in base64</param>
        /// <returns></returns>
        public Message ExecuteExternalTransfer(Transfer transfer, string accountTo, string credentials)
        {
            return Channel.ExecuteExternalTransfer(transfer, accountTo, credentials);
        }

        /// <summary>
        /// Async version of <see cref="ExecuteExternalTransfer"/>
        /// </summary>
        /// <param name="transfer">Required data to transfer</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Users credentials encoded in base64</param>
        /// <returns></returns>
        public Task<Message> ExecuteExternalTransferAsync(Transfer transfer, string accountTo, string credentials )
        {
            return Channel.ExecuteExternalTransferAsync(transfer, accountTo, credentials);
        }

        /// <summary>
        /// Execute internal transfer
        /// </summary>
        /// <param name="transfer">Required data to transfer</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Users credentials encoded in base64</param>
        /// <returns></returns>
        public Message ExecuteInternalTransfer(Transfer transfer, string accountTo, string credentials)
        {
            return Channel.ExecuteInternalTransfer(transfer, accountTo, credentials);
        }

        /// <summary>
        /// Async version of <see cref="ExecuteInternalTransfer"/>
        /// </summary>
        /// <param name="transfer">Required data to transfer</param>
        /// <param name="accountTo">Destination bank account number</param>
        /// <param name="credentials">Users credentials encoded in base64</param>
        /// <returns></returns>
        public Task<Message> ExecuteInternalTransferAsync(Transfer transfer, string accountTo, string credentials)
        {
            return Channel.ExecuteInternalTransferAsync(transfer, accountTo, credentials);
        }

        /// <summary>
        /// Get users operation history
        /// </summary>
        /// <param name="credentials">Base64 Users credentials</param>
        /// <returns>List of History class items</returns>
        public List<History> GetUsersHistory(string credentials)
        {
            return Channel.GetUsersHistory(credentials);
        }

        /// <summary>
        /// Async version of <see cref="GetUsersHistory"/>
        /// </summary>
        /// <param name="credentials">Base64 Users credentials</param>
        /// <returns>List of History class items</returns>
        public Task<List<History>> GetUsersHistoryAsync(string credentials)
        {
            return Channel.GetUsersHistoryAsync(credentials);
        }

        /// <summary>
        /// Creating Bank account for user
        /// </summary>
        /// <param name="credentials">Base64 encoded users credenetials</param>
        /// <returns></returns>
        public Message CreateBankAccount(string credentials)
        {
            return Channel.CreateBankAccount(credentials);
        }

        /// <summary>
        /// Async version of <see cref="CreateBankAccount"/>
        /// </summary>
        /// <param name="credentials">Base64 encoded users credenetials</param>
        /// <returns></returns>
        public Task<Message> CreateBankAccountAsync(string credentials)
        {
            return Channel.CreateBankAccountAsync(credentials);
        }
    }
}