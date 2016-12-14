using System.ServiceModel;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client
{
    class Proxy : ClientBase<IService1>, IService1
    {
        Message IService1.GetBankAccountNumber( User user )
        {
            return Channel.GetBankAccountNumber(user);
        }

        Task<Message> IService1.GetBankAccountNumberAsync(User user)
        {
            return Channel.GetBankAccountNumberAsync(user);
        }

        public Message AddUser(string login, string password)
        {
            return Channel.AddUser(login, password);
        }

        public Task<Message> AddUserAsync(string login, string password)
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
    }
}
