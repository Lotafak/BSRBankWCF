using System.Collections.Generic;
using System.ServiceModel;
using BSRBankWCF.Models;

namespace BSRBankWCF.Services
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Account> GetUserAccounts( string credentials);

        [OperationContract]
        Message AddUser( string login, string password );

        [OperationContract]
        Message ValidateUser( string login, string password );

        [OperationContract]
        Message WithdrawDepositMoney(WithdrawDeposit withdrawDeposit);

        [OperationContract]
        Message ExecuteExternalTransfer(Transfer transfer, string accountTo, string credentials);

        [OperationContract]
        Message ExecuteInternalTransfer(Transfer transfer, string accountTo, string credentials);

        [OperationContract]
        List<History> GetUsersHistory(string credentials);

        [OperationContract]
        Message CreateBankAccount(string credentials);
    }
}