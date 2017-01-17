using System.Windows.Controls;
using Client.ServiceReference1;

namespace Client.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly ResourceWrapper _resourceWrapper = new ResourceWrapper();
        public MainPage()
        {
            InitializeComponent();
            BankAccountNumber.Header = _resourceWrapper.BankAccountNumberListViewHeader;
            BankAccountCondition.Header = _resourceWrapper.BankAccountConditionListViewHeader;
            RefreshGridview();
        }

        public Account GetSelectedItem()
        {
            return (Account)AccountGridView.SelectedItem;
        }

        public void RefreshGridview()
        {
            var proxy = new Proxy.Proxy();
            AccountGridView.ItemsSource = proxy.GetUserAccounts(Windows.MainWindow.Credentials);
            AccountGridView.Items.Refresh();
        }
    }
}
