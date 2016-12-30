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
        public TransferPage()
        {
            InitializeComponent();
        }

        public void InsertBankAccount( string bankAccountNumber )
        {
            AccountFromTextBox.Text = bankAccountNumber;
        }

        private void GoBackButton_OnClick( object sender, RoutedEventArgs e )
        {
            var ns = NavigationService.GetNavigationService(GoBackButton);

            if ( ns == null ) return;

            if ( ns.CanGoBack )
                ns.GoBack();
        }

        private void DoTransferButton_OnClick( object sender, RoutedEventArgs e )
        {
            if(!CheckTransferParameters())
                return;

            var transfer = new Transfer
            {
                Amount = int.Parse(AmountTextBox.Text),
                From = AccountFromTextBox.Text,
                Title = TitleTextBox.Text
            };

            var proxy = new Proxy.Proxy();
            var respondMessage = proxy.ExecuteExternalTransfer(transfer, AccountToTextBox.Text, MainWindow.Credentials);
            ClientUtils.ShowMessage(respondMessage);

            var ns = NavigationService.GetNavigationService(DoTransferButton);

            if(ns == null) return;

            if(ns.CanGoBack)
                ns.GoBack();
        }

        private bool CheckTransferParameters()
        {
            var proxy = new Proxy.Proxy();
            if ( !AccountUtils.ValidateAccountNumber(AccountFromTextBox.Text) ||
                proxy.GetUserAccounts(MainWindow.Credentials).All(x => x.BankAccountNumber != AccountFromTextBox.Text))
            {
                ClientUtils.ShowMessage(new ErrorMessage("Niepoprawny numer konta bankowego nadawcy"));
                return false;
            }
            if ( !AccountUtils.ValidateAccountNumber(AccountToTextBox.Text) )
            {
                ClientUtils.ShowMessage(new ErrorMessage("Niepoprawny numer konta bankowego odbiorcy"));
                return false;
            }
            if (AccountFromTextBox.Text == AccountToTextBox.Text)
            {
                ClientUtils.ShowMessage(new ErrorMessage("Konto odbiory i nadawcy nie mogą być takie same" ));
                return false;
            }
            if ( TitleTextBox.Text == "" )
            {
                ClientUtils.ShowMessage(new ErrorMessage("Proszę wypełnić polę \"Tytuł\""));
                return false;
            }

            decimal amountDec;
            if (!decimal.TryParse(AmountTextBox.Text.Replace(".", ","), out amountDec) || amountDec <= 0)
            {
                ClientUtils.ShowMessage(new ErrorMessage("Niepoprawna kwota przelewu"));
                return false;
            }

            return true;
        }
    }
}
