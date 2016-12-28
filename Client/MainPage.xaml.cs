using System.Windows.Controls;
using Client.ServiceReference1;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            RefreshGridview();
        }

        public Account GetSelectedItem()
        {
            return (Account)AccountGridView.SelectedItem;
        }

        public void RefreshGridview()
        {
            var proxy = new Proxy();
            AccountGridView.ItemsSource = proxy.GetBankAccounts(MainWindow.Credentials);
            AccountGridView.Items.Refresh();
        }
    }
}
