using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference1;

namespace Client
{
    class Proxy : ClientBase<IService1>, IService1
    {
        public string GetBankAccountNumber(User user)
        {
            return Channel.GetBankAccountNumber(user);
        }

        public Task<string> GetBankAccountNumberAsync(User user)
        {
            throw new NotImplementedException();
        }

        public bool AddUser(User user)
        {
            return Channel.AddUser(user);
        }

        public Task<bool> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public string Hello()
        {
            return Channel.Hello();
        }

        public Task<string> HelloAsync()
        {
            return Channel.HelloAsync();
        }
    }
}
