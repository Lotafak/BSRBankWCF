using System.Collections.Generic;
using System.Windows;
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Keeps Base64 encrypted credentials within MainWindow Context
        /// </summary>
        public static string Credentials { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            var proxy = new Proxy();
        }

        /// <summary>
        /// Handles succesful login callback
        /// </summary>
        /// <param name="e">Contains information about credentials (login, password)></param>
        public void loginWindow_LoginSuccesful(object sender, LoginWindowEventArgs e)
        {
            var proxy = new Proxy();
            Credentials = ClientUtils.Base64Encode($"{e.Username}:{e.Password}");
            accountGridView.ItemsSource = proxy.GetBankAccounts(Credentials);
        }

        ///<summary>
        /// Deposit button handler
        /// </summary> 
        private void DepositButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Getting and checking selected item in GridView
            var x = (Account)accountGridView.SelectedItem;

            // When no item is selected in GridView do nothing
            if (x == null)
                return;

            ProgressBar.Visibility = Visibility.Visible;

            // Establish connection with service
            var proxy = new Proxy();
            var result = proxy.DepositMoney(Credentials, 2, x.BankAccountNumber);

            // Refresh GridView
            accountGridView.ItemsSource = proxy.GetBankAccounts(Credentials);
            accountGridView.Items.Refresh();

            ProgressBar.Visibility = Visibility.Hidden;
            ClientUtils.ShowMessage(result);
        }

        ///<summary>
        /// Withdraw button handler
        /// </summary> 
        private void WithdrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Getting and checking selected item in GridView
            var selectedAccount = (Account)accountGridView.SelectedItem;

            // When no item is selected in GridView do nothing
            if ( selectedAccount == null )
                return;

            ProgressBar.Visibility = Visibility.Visible;

            // Establish connection with service
            var proxy = new Proxy();
            var result = proxy.WithdrawMoney(Credentials, 2, selectedAccount.BankAccountNumber);

            // Refresh GridView
            //accountGridView.ItemsSource = null;
            accountGridView.ItemsSource = proxy.GetBankAccounts(Credentials);
            accountGridView.Items.Refresh();

            ProgressBar.Visibility = Visibility.Hidden;
            ClientUtils.ShowMessage(result);
        }
    }
}