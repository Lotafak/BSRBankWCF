using System.Runtime.Serialization;
using System.ServiceModel;
using BSRBankWCF.Models;

namespace BSRBankWCF
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetBankAccountNumber( User user );

        [OperationContract]
        bool AddUser(User user);

        //database.GetCollection<Listen>(Constants.ListenCollection).InsertMany( listenSet,new InsertManyOptions {IsOrdered = false});
        [OperationContract]
        string Hello();
    }
}
