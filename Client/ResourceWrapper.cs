using System.Resources;
using Client.Windows;

namespace Client
{
    /// <summary>
    /// Class for accessing resources from Resources/Res.resx file 
    /// </summary>
    public class ResourceWrapper
    {
        private readonly ResourceManager _rm;

        /// <summary>
        /// Default constructor initializing ResourceManager
        /// </summary>
        public ResourceWrapper()
        {
            _rm = new ResourceManager("Client.Resource.Res-pl", typeof(MainWindow).Assembly);
        }

        public string InternalTransfer => _rm.GetString("InternalTransfer");
        public string ExternalTransfer => _rm.GetString("ExternalTransfer");
        public string InvalidBankAccountFrom => _rm.GetString("InvalidBankAccountFrom");
        public string InvalidBankAccountTo => _rm.GetString("InvalidBankAccountTo");
        public string SameFromToBankAccounts => _rm.GetString("SameFromToBankAccounts");
        public string NoTitleError => _rm.GetString("NoTitleError");
        public string WrongAmount => _rm.GetString("WrongAmount");

        public string WrongLoginParameters => _rm.GetString("WrongLoginParameters");

        public string BankAccountNumberListViewHeader => _rm.GetString("BankAccountNumberListViewHeader");
        public string BankAccountConditionListViewHeader => _rm.GetString("BankAccountConditionListViewHeader");

        public string HistoryOperationTypeListViewHeader => _rm.GetString("HistoryOperationTypeListViewHeader");
        public string HistoryFromListViewHeader => _rm.GetString("HistoryFromListViewHeader");
        public string HistoryToListViewHeader => _rm.GetString("HistoryToListViewHeader");
        public string HistoryAmountListViewHeader => _rm.GetString("HistoryAmountListViewHeader");
        public string HistoryDateListViewHeader => _rm.GetString("HistoryDateListViewHeader");

        public string TransferFrom => _rm.GetString("TransferFrom");
        public string TransferTo => _rm.GetString("TransferTo");
        public string TransferAmount => _rm.GetString("TransferAmount");
        public string TransferTitle => _rm.GetString("TransferTitle");
        public string TransferGoBack => _rm.GetString("TransferGoBack");
        public string TransferExecuteTransfer => _rm.GetString("TransferExecuteTransfer");

        public string MainWithdrawMoney => _rm.GetString("MainWithdrawMoney");
        public string MainLoggedAs => _rm.GetString("MainLoggedAs");
        public string MainHome => _rm.GetString("MainHome");
        public string MainHistory => _rm.GetString("MainHistory");
        public string MainDepositMoney => _rm.GetString("MainDepositMoney");
        public string MainCreateAccount => _rm.GetString("MainCreateAccount");
    }
}
