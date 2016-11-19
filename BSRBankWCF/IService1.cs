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
        void AddUser(User user);

        //database.GetCollection<Listen>(Constants.ListenCollection).InsertMany( listenSet,new InsertManyOptions {IsOrdered = false});
    }
}
