using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client.Proxy
{
    internal class Proxy : ClientBase<IService1>, IService1
    {
        public List<Account> GetUserAccounts( string credentials)
        {
            return Channel.GetUserAccounts(credentials);
        }

        public Task<List<Account>> GetUserAccountsAsync( string credentials)
        {
            return Channel.GetUserAccountsAsync(credentials);
        }

        public Message AddUser(string login, string password)
        {
            return Channel.AddUser(login,password);
        }

        public Task<Message> AddUserAsync( string login, string password )
        {
            return Channel.AddUserAsync(login, password);
        }

        public Message ValidateUser( string login, string password )
        {
            return Channel.ValidateUser(login, password);
        }

        public Task<Message> ValidateUserAsync( string login, string password )
        {
            return Channel.ValidateUserAsync(login, password);
        }

        public Message WithdrawDepositMoney(WithdrawDeposit withdrawDeposit)
        {
            return Channel.WithdrawDepositMoney(withdrawDeposit);
        }

        public Task<Message> WithdrawDepositMoneyAsync(WithdrawDeposit withdrawDeposit)
        {
            return Channel.WithdrawDepositMoneyAsync(withdrawDeposit);
        }

        public Message ExecuteExternalTransfer(Transfer transfer, string accountTo, string credentials)
        {
            return Channel.ExecuteExternalTransfer(transfer, accountTo, credentials);
        }

        public Task<Message> ExecuteExternalTransferAsync(Transfer transfer, string accountTo, string credentials )
        {
            return Channel.ExecuteExternalTransferAsync(transfer, accountTo, credentials);
        }

        public List<History> GetUsersHistory(string credentials)
        {
            return Channel.GetUsersHistory(credentials);
        }

        public Task<List<History>> GetUsersHistoryAsync(string credentials)
        {
            return Channel.GetUsersHistoryAsync(credentials);
        }
    }
}