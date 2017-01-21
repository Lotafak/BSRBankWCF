using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Client.ServiceReference1;
using Client.Utils;
using Client.Windows;

namespace Client.Pages
{
    /// <summary>
    /// Interaction logic for TransferPage.xaml
    /// </summary>
    public partial class TransferPage
    {
        private readonly bool _isExternal;
        private readonly ResourceWrapper _resourceWrapper = new ResourceWrapper();

        // Private constructor not allowed to access from within another class. 
        // The reason is we want to know whether to create internal or external transfer page.
        private TransferPage()
        {
            InitializeComponent();
            TransferFromLabel.Content = _resourceWrapper.TransferFrom;
            TransferToLabel.Content = _resourceWrapper.TransferTo;
            TransferAmountLabel.Content = _resourceWrapper.TransferAmount;
            TransferTitleLabel.Content = _resourceWrapper.TransferTitle;
            GoBackButton.Content = _resourceWrapper.TransferGoBack;
            ExecuteTransferButton.Content = _resourceWrapper.TransferExecuteTransfer;
        }

        /// <summary>
        /// Creates Internal or External Transfer Page 
        /// </summary>
        /// <param name="isExt">Indicates whether to create external or internal transfer page</param>
        public TransferPage(bool isExt ) : this()
        {
            _isExternal = isExt;
            TransferLabel.Content = isExt ? 
                _resourceWrapper.ExternalTransfer : _resourceWrapper.InternalTransfer;
        }

        /// <summary>
        /// Fill Text property of AccountFromTextBox. 
        /// </summary>
        /// <param name="bankAccountNumber">26 digit users bank account number</param>
        public void InsertBankAccount( string bankAccountNumber )
        {
            AccountFromTextBox.Text = bankAccountNumber;
        }

        /// <summary>
        /// Call NavigationService to go back if possible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoBackButton_OnClick( object sender, RoutedEventArgs e )
        {
            var ns = NavigationService.GetNavigationService(GoBackButton);

            if ( ns == null ) return;

            if ( ns.CanGoBack )
                ns.GoBack();
        }

        /// <summary>
        /// Execute external or internal transfer call to SOAP Service. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoTransfer( object sender, RoutedEventArgs e )
        {
            if(!CheckTransferParameters())
                return;

            //var sb = new StringBuilder(TitleTextBox.Text);

            var transfer = new Transfer
            {
                Amount = int.Parse(AmountTextBox.Text),
                From = AccountFromTextBox.Text,
                Title = TitleTextBox.Text
            };

            var proxy = new Proxy.Proxy();

            // Invoke internal or external transfer call
            var respondMessage = _isExternal ? 
                proxy.ExecuteExternalTransfer(transfer, AccountToTextBox.Text, MainWindow.Credentials) : 
                proxy.ExecuteInternalTransfer(transfer, AccountToTextBox.Text, MainWindow.Credentials);

            ClientUtils.ShowMessage(respondMessage);

            // Go back to main window
            var ns = NavigationService.GetNavigationService(ExecuteTransferButton);

            if(ns == null) return;

            if(ns.CanGoBack)
                ns.GoBack();
        }

        private bool CheckTransferParameters()
        {
            var proxy = new Proxy.Proxy();
            if ( !AccountUtils.ValidateAccountNumber(AccountFromTextBox.Text) ||
                proxy.GetUserAccounts(MainWindow.Credentials).FirstOrDefault(x => x.BankAccountNumber == AccountFromTextBox.Text) == null)
            {
                ClientUtils.ShowMessage(new ErrorMessage(_resourceWrapper.InvalidBankAccountFrom));
                return false;
            }
            if ( !AccountUtils.ValidateAccountNumber(AccountToTextBox.Text) )
            {
                ClientUtils.ShowMessage(new ErrorMessage(_resourceWrapper.InvalidBankAccountTo));
                return false;
            }
            if (AccountFromTextBox.Text == AccountToTextBox.Text)
            {
                ClientUtils.ShowMessage(new ErrorMessage(_resourceWrapper.SameFromToBankAccounts));
                return false;
            }
            if ( TitleTextBox.Text == "" )
            {
                ClientUtils.ShowMessage(new ErrorMessage(_resourceWrapper.NoTitleError));
                return false;
            }

            decimal amountDec;
            if (decimal.TryParse(AmountTextBox.Text.Replace(".", ","), out amountDec) && amountDec > 0) return true;

            ClientUtils.ShowMessage(new ErrorMessage(_resourceWrapper.WrongAmount));
            return false;
        }
    }
}
