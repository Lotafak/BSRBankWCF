using System;
using System.Windows;
using System.Windows.Controls;
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Keeps Base64 encrypted credentials within MainWindow Context
        /// </summary>
        public static string Credentials { get; private set; }

        private MainPage gridViewPage;

        public MainWindow()
        {
            InitializeComponent();

            var proxy = new Proxy();
        }

        /// <summary>
        ///     Handles succesful login callback
        /// </summary>
        /// <param name="e">Contains information about credentials (login, password)></param>
        public void loginWindow_LoginSuccesful( object sender, LoginWindowEventArgs e )
        {
            Credentials = ClientUtils.Base64Encode($"{e.Username}:{e.Password}");
            gridViewPage = new MainPage();
            Main.Content = gridViewPage;
        }

        /// <summary>
        ///     Withdraw and Deposit button handler
        /// </summary>
        private void WithdrawDepositButton_OnClick( object sender, RoutedEventArgs e )
        {
            // Getting and checking selected item in GridView
            var selectedAccount = gridViewPage.GetSelectedItem();
            var button = sender as Button;
            Message result = null;

            // When no item is selected in GridView do nothing
            if ( selectedAccount == null )
                return;

            // Get amount money to withdraw/deposit
            var input = Microsoft.VisualBasic.Interaction.InputBox("Proszę podać kwotę wpłaty/wypłaty", "Wpłata/wypłata");

            if ( input == "" )
                return;

            // It is possible that user enters wrong data
            decimal amount;
            var parse = decimal.TryParse(input.Replace('.', ','), out amount);
            if ( !parse || amount > long.MaxValue || amount <= 0 )
            {
                // If they do, show message and do nothing.
                ClientUtils.ShowMessage(new ErrorMessage { IsError = true, MessageText = "Podano błędną kwotę !" });
                return;
            }

            // Show to the user that program is working
            ProgressBar.Visibility = Visibility.Visible;

            // Establish connection with service
            var proxy = new Proxy();

            // Regarding which button was pressed invoking WithdrawDeposit method with Deposit or Withdraw object 
            if ( button?.Name == DepositButton.Name )
                result = proxy.WithdrawDepositMoney(new Deposit
                {
                    Amount = amount,
                    BankAccountNumber = selectedAccount.BankAccountNumber,
                    Credentials = Credentials
                });
            else if ( button?.Name == WithdrawButton.Name )
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
            gridViewPage.RefreshGridview();

            // Program stopped working
            ProgressBar.Visibility = Visibility.Hidden;
            ClientUtils.ShowMessage(result);

            Main.Content = gridViewPage;
        }

        private void ExternalTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            // Getting and checking selected item in GridView
            var selectedAccount = gridViewPage.GetSelectedItem();
            var button = sender as Button;
            Message result = null;

            // When no item is selected in GridView do nothing
            if ( selectedAccount == null )
            {
                ClientUtils.ShowMessage(new ErrorMessage { IsError = true, MessageText = "Proszę wybrać konto źródłowe" });
                return;
            }
        }

        private void InternalTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Both external and internal transfers
        }
    }
}