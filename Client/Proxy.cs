using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client
{
    internal class Proxy : ClientBase<IService1>, IService1
    {
        public List<Account> GetBankAccounts(string credentials)
        {
            return Channel.GetBankAccounts(credentials);
        }

        public Task<List<Account>> GetBankAccountsAsync(string credentials)
        {
            return Channel.GetBankAccountsAsync(credentials);
        }

        public Message AddUser(string login, string password)
        {
            return Channel.AddUser(login, password);
        }

        public Task<Message> AddUserAsync(string login, string password)
        {
            return Channel.AddUserAsync(login, password);
        }

        public Message ValidateUser(string login, string password)
        {
            return Channel.ValidateUser(login, password);
        }

        public Task<Message> ValidateUserAsync(string login, string password)
        {
            return Channel.ValidateUserAsync(login, password);
        }
    }
}