using System;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public event EventHandler<LoginWindowEventArgs> DialogFinished;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void regiser_button_Click( object sender, RoutedEventArgs e )
        {
            if (login_textBox.Text != "" && password_passwordBox.Password != "")
            {
                DialogFinished?.Invoke(this,
                    new LoginWindowEventArgs(password_passwordBox.Password, login_textBox.Text));
                Close();
            } 
            else if (login_textBox.Text == "")
                MessageBox.Show("Uzupełnij pole \"Login\"");
            else if(password_passwordBox.Password == "")
                MessageBox.Show("Uzupełnij pole \"Hasło\"");
        }

        private void cancel_button_Click( object sender, RoutedEventArgs e )
        {
            Close();
        }
    }
}
