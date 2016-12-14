using System.ServiceModel;
using BSRBankWCF.Models;

namespace BSRBankWCF
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Message GetBankAccountNumber( User user );

        [OperationContract]
        Message AddUser(string login, string password);

        [OperationContract]
        Message ValidateUser(string login, string password);
    }
}
