using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            
            Proxy proxy = new Proxy();
            //label.Content = proxy.Hello();
        }

        void loginWindow_DialogFinished(object sender, LoginWindowEventArgs e)
        {
            //MessageBox.Show("DialogFinished");
            Proxy proxy = new Proxy();
            bool success = proxy.AddUser(new User()
            {
                Password = e.Password,
                Name = e.Username
            });
            MessageBox.Show(success ? "Użytkownik dodany !" : "Somethings went wrong :(");
        }

        private void login_button_Copy_Click( object sender, RoutedEventArgs e )
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.DialogFinished += new EventHandler<LoginWindowEventArgs>(loginWindow_DialogFinished);
            loginWindow.Show();
        }
    }
}
