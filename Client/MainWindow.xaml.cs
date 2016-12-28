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
        public static string Credentials { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            //accountGridView.ItemsSource = new List<Account> { new Account { Amount = 5000, BankAccountNumber = "12382163810"}, new Account { Amount = 2000, BankAccountNumber = "123615236"} };

            var proxy = new Proxy();
        }

        public void loginWindow_LoginSuccesful(object sender, LoginWindowEventArgs e)
        {
            var proxy = new Proxy();
            Credentials = ClientUtils.Base64Encode($"{e.Username}:{e.Password}");
            accountGridView.ItemsSource = proxy.GetBankAccounts(Credentials);
        }

        private void login_button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.DialogFinished += loginWindow_LoginSuccesful;
            loginWindow.Show();
        }

        private void DepositButton_OnClick(object sender, RoutedEventArgs e)
        {
            //var (User) accountGridView.SelectedItem;
        }
    }
}