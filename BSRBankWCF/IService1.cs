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

        //database.GetCollection<Listen>(Constants.ListenCollection).InsertMany( listenSet,new InsertManyOptions {IsOrdered = false});
        [OperationContract]
        string Hello();

        [OperationContract]
        //Message ValidateUser(string login, string password);
        Message ValidateUser(string login, string password);
    }
}
