using System;
using System.Collections.Generic;
using System.ServiceModel;
using BSRBankWCF.Models;

namespace BSRBankWCF
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Account> GetBankAccounts(string credentials);

        [OperationContract]
        Message AddUser(string login, string password);

        [OperationContract]
        Message ValidateUser(string login, string password);

        [OperationContract]
        Message DepositMoney(string credentials, int amount, string accountNumber);
        [OperationContract]
        Message WithdrawMoney(string credentials, int amount, string accountNumber);
    }
}