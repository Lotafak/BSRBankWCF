using System;
using System.Windows;
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow mainWindow = new MainWindow();
        public event EventHandler<LoginWindowEventArgs> DialogFinished;
        
        public LoginWindow()
        {
            InitializeComponent();
            DialogFinished += mainWindow.loginWindow_DialogFinished;
        }

        private void regiser_button_OnClick( object sender, RoutedEventArgs e )
        {
            // TODO: Some feedback about register status...
            var password = password_passwordBox.Password;
            var login = login_textBox.Text;

            if (login != "" && password != "")
            {
                var proxy = new Proxy();
                var success = proxy.AddUser(login, password);
                //MessageBox.Show(success ? "Użytkownik dodany pomyślnie\n\nMożna się zalogować" : "Somethings went wrong :(");
                password_passwordBox.Password = "";
                login_textBox.Text = "";
            } 
            else if (login_textBox.Text == "")
                MessageBox.Show("Uzupełnij pole \"Login\"");
            else if(password_passwordBox.Password == "")
                MessageBox.Show("Uzupełnij pole \"Hasło\"");
        }

        private void cancel_button_OnClick( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void Login_button_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Pass the data to main window, extract strings to resources (everywhere actually)
            var password = password_passwordBox.Password;
            var login = login_textBox.Text;

            if (login != "" && password != "")
            {
                var proxy = new Proxy();
                var message = proxy.ValidateUser(login, password);
                if (message.IsError)
                {
                    MessageBox.Show(message.MessageText);
                    return;
                }

                DialogFinished?.Invoke(this,
                    new LoginWindowEventArgs(password, login));
                mainWindow.Show();
                Close();
            }
            else if ( login_textBox.Text == "" )
                MessageBox.Show("Uzupełnij pole \"Login\"");
            else if ( password_passwordBox.Password == "" )
                MessageBox.Show("Uzupełnij pole \"Hasło\"");
        }
    }
}
