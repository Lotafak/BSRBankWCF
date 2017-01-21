using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Client.Utils;

namespace Client.Windows
{
    /// <summary>
    ///     Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private readonly MainWindow _mainWindow = new MainWindow();
        private readonly ResourceWrapper _resourceWrapper = new ResourceWrapper();

        /// <summary>
        /// LoginWindows constructor=
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();

            // Attaching event to MainWindow created in background
            DialogFinished += _mainWindow.loginWindow_LoginSuccesful;
        }

        /// <summary>
        /// EventHandler handling Loginwindow succesfull login
        /// </summary>
        public event EventHandler<LoginWindowEventArgs> DialogFinished;

        private void regiser_button_OnClick( object sender, RoutedEventArgs e )
        {
            if ( !CheckLoginParameters() )
                return;

            // Register client 
            var proxy = new Proxy.Proxy();
            var message = proxy.AddUser(login_textBox.Text, password_passwordBox.Password);

            // Show return message (Wheather it is success or failure)
            ClientUtils.ShowMessage(message);

            // If something goes wrong don't reset Login and Password fields
            if ( message.IsError ) return;

            password_passwordBox.Password = "";
            login_textBox.Text = "";
        }

        private void cancel_button_OnClick( object sender, RoutedEventArgs e )
        {
            _mainWindow.Close();
            Close();
        }

        // Fires up when windows is LoginWindow is closing
        private void Window_Closing( object sender, CancelEventArgs e )
        {
            var wasCodeClosed =
                new StackTrace().GetFrames()?.FirstOrDefault(x => x.GetMethod() == typeof(Window).GetMethod("Close")) !=
                null;

            // If window was closed by code (cancel button)
            // MainWindow object needs to be destroyed
            if ( !wasCodeClosed )
                _mainWindow.Close();
        }

        private void Login_button_OnClick( object sender, RoutedEventArgs e )
        {
            // Get data from Window
            var password = password_passwordBox.Password;
            var login = login_textBox.Text;

            // Check parameters
            if ( !CheckLoginParameters() )
                return;
            
            // Let service validate user
            var proxy = new Proxy.Proxy();
            var message = proxy.ValidateUser(login, password);
            if ( message.IsError )
            {
                // Show service mesasge
                ClientUtils.ShowMessage(message);
                password_passwordBox.Password = "";
                return;
            }

            // If everythings fine send data to MainWindow, show the window ...
            DialogFinished?.Invoke(this, new LoginWindowEventArgs(message.MessageText));
            _mainWindow.Show();
            // ... and close LoginWindow
            Close();
        }

        private bool CheckLoginParameters()
        {
            if ( password_passwordBox.Password != "" && login_textBox.Text != "" ) return true;

            MessageBox.Show(_resourceWrapper.WrongLoginParameters);
            return false;
        }
    }
}