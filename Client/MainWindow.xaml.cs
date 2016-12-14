using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string Credentials;

        public MainWindow()
        {
            InitializeComponent();
            
            var proxy = new Proxy();
            //label.Content = proxy.Hello();
        }

        public void loginWindow_LoginSuccesful(object sender, LoginWindowEventArgs e)
        {
            MessageBox.Show("DialogFinished");

            //var proxy = new Proxy();
            //var success = proxy.AddUser(new User()
            //{
            //    Password = e.Password,
            //    Name = e.Username
            //});
            //MessageBox.Show(success ? "Użytkownik dodany !" : "Somethings went wrong :(");
        }

        private void login_button_Copy_Click( object sender, RoutedEventArgs e )
        {
            var loginWindow = new LoginWindow();
            loginWindow.DialogFinished += loginWindow_LoginSuccesful;
            loginWindow.Show();
        }
    }
}
