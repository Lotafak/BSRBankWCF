using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Client.ServiceReference1;
using Client.Utils;

namespace Client.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        ///     Keeps Base64 encrypted credentials within MainWindow Context
        /// </summary>
        public static string Credentials { get; private set; }

        private Pages.MainPage _gridViewPage;

        public MainWindow()
        {
            InitializeComponent();

            Main.NavigationService.Navigating += NavigationServiceOnNavigating;
        }

        private void NavigationServiceOnNavigating(object sender, NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            _gridViewPage.RefreshGridview();
        }

        /// <summary>
        ///     Handles succesful login callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Contains credentials</param>
        public void loginWindow_LoginSuccesful(object sender, LoginWindowEventArgs e)
        {
            Credentials = e.Credentials;
            LoggedAsLabel.Content += AccountUtils.LoginFromCredentials(Credentials);
            _gridViewPage = new Pages.MainPage();
            Main.Content = _gridViewPage;
        }

        /// <summary>
        ///     Withdraw and Deposit button handler
        /// </summary>
        private void WithdrawDepositButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Getting and checking selected item in GridView
            var selectedAccount = _gridViewPage.GetSelectedItem();
            var button = sender as Button;
            Message result = null;

            // When no item is selected in GridView do nothing
            if (selectedAccount == null)
                return;

            // Get amount money to withdraw/deposit
            var input = Microsoft.VisualBasic.Interaction.InputBox("Proszę podać kwotę wpłaty/wypłaty", "Wpłata/wypłata");

            if (input == "")
                return;

            // It is possible that user enters wrong data
            decimal amount;
            var parse = decimal.TryParse(input.Replace('.', ','), out amount);
            if (!parse || amount > long.MaxValue || amount <= 0)
            {
                // If they do, show message and do nothing.
                ClientUtils.ShowMessage(new ErrorMessage {IsError = true, MessageText = "Podano błędną kwotę !"});
                return;
            }

            // Show to the user that program is working
            ProgressBar.Visibility = Visibility.Visible;

            // Establish connection with service
            var proxy = new Proxy.Proxy();

            // Regarding which button was pressed invoking WithdrawDeposit method with Deposit or Withdraw object 
            if (button?.Name == DepositButton.Name)
                result = proxy.WithdrawDepositMoney(new Deposit
                {
                    Amount = amount,
                    BankAccountNumber = selectedAccount.BankAccountNumber,
                    Credentials = Credentials
                });
            else if (button?.Name == WithdrawButton.Name)
            {
                var withdraw = new Withdraw()
                {
                    Amount = amount,
                    BankAccountNumber = selectedAccount.BankAccountNumber,
                    Credentials = Credentials
                };
                result = proxy.WithdrawDepositMoney(withdraw);
            }

            // Refresh GridView
            _gridViewPage.RefreshGridview();

            // Program stopped working
            ProgressBar.Visibility = Visibility.Hidden;
            ClientUtils.ShowMessage(result);

            Main.Content = _gridViewPage;
        }

        private void ExternalTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            // Getting and checking selected item in GridView
            var selectedAccount = _gridViewPage.GetSelectedItem();
            var transferPage = new Pages.TransferPage();

            // When no item is selected in GridView do nothing
            if (selectedAccount != null) transferPage.InsertBankAccount(selectedAccount.BankAccountNumber);

            Main.Content = transferPage;
        }

        private void InternalTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            var webClient = new WebClient
            {
                Headers = {["Content-type"] = "application/json"},
                Encoding = Encoding.UTF8
            };
            var s =
                webClient.DownloadString(
                    "http://localhost:8733/Design_Time_Addresses/BSRBankWCF/RestService/accounts/11");
            MessageBox.Show(s);
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            _gridViewPage = new Pages.MainPage();
            Main.Content = _gridViewPage;
        }

        private void HistoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            var historyPage = new Pages.HistoryPage();
            Main.Content = historyPage;
        }
    }
}