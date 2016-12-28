using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Client
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly MainWindow _mainWindow = new MainWindow();

        public LoginWindow()
        {
            InitializeComponent();
            DialogFinished += _mainWindow.loginWindow_LoginSuccesful;
        }

        public event EventHandler<LoginWindowEventArgs> DialogFinished;

        private void regiser_button_OnClick(object sender, RoutedEventArgs e)
        {
            var password = password_passwordBox.Password;
            var login = login_textBox.Text;

            if (login != "" && password != "")
            {
                // Register client 
                var proxy = new Proxy();
                var message = proxy.AddUser(login, password);

                // Show return message (Wheather it is success or failure)
                ClientUtils.ShowMessage(message);

                // If something goes wrong don't reset Login and Password fields
                if (message.IsError) return;

                password_passwordBox.Password = "";
                login_textBox.Text = "";
            }
            else if (login_textBox.Text == "")
                MessageBox.Show(Properties.Resources.LackOfLoginMessageBoxText);
            else if (password_passwordBox.Password == "")
                MessageBox.Show(Properties.Resources.LackOfPasswordMessageBoxText);
        }

        private void cancel_button_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.Close();
            Close();
        }

        // Fires up when windows is LoginWindow is closing
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var wasCodeClosed =
                new StackTrace().GetFrames().FirstOrDefault(x => x.GetMethod() == typeof(Window).GetMethod("Close")) !=
                null;

            // If window was closed by code (cancel button)
            // MainWindow object needs to be destroyed
            if (!wasCodeClosed)
                _mainWindow.Close();
        }

        private void Login_button_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Pass the data to main window
            var password = password_passwordBox.Password;
            var login = login_textBox.Text;

            if (login != "" && password != "")
            {
                var proxy = new Proxy();
                var message = proxy.ValidateUser(login, password);
                if (message.IsError)
                {
                    ClientUtils.ShowMessage(message);
                    password_passwordBox.Password = "";
                    return;
                }

                DialogFinished?.Invoke(this,
                    new LoginWindowEventArgs(password, login));
                _mainWindow.Show();
                Close();
            }
            else if (login_textBox.Text == "")
                MessageBox.Show(Properties.Resources.LackOfLoginMessageBoxText);
            else if (password_passwordBox.Password == "")
                MessageBox.Show(Properties.Resources.LackOfPasswordMessageBoxText);
        }
    }
}