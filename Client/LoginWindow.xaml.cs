﻿using System;
using System.Windows;
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly MainWindow _mainWindow = new MainWindow();
        public event EventHandler<LoginWindowEventArgs> DialogFinished;
        
        public LoginWindow()
        {
            InitializeComponent();
            DialogFinished += _mainWindow.loginWindow_LoginSuccesful;
        }

        private void regiser_button_OnClick( object sender, RoutedEventArgs e )
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
            else if(password_passwordBox.Password == "")
                MessageBox.Show(Properties.Resources.LackOfPasswordMessageBoxText);
        }

        private void cancel_button_OnClick( object sender, RoutedEventArgs e )
        {
            // MainWindow object also needs to be destroyed
            _mainWindow.Close();

            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // MainWindow object needs to be destroyed
            //_mainWindow.Close();

            // TODO: Close main window only when exiting on purpose, not closing the loginWindow 
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
                //Close();
            }
            else if ( login_textBox.Text == "" )
                MessageBox.Show(Properties.Resources.LackOfLoginMessageBoxText);
            else if ( password_passwordBox.Password == "" )
                MessageBox.Show(Properties.Resources.LackOfPasswordMessageBoxText);
        }
    }
}
