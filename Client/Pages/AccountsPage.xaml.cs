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

        /// <summary>
        /// Initializing components, headers and refreshing GridView items
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BankAccountNumber.Header = _resourceWrapper.BankAccountNumberListViewHeader;
            BankAccountCondition.Header = _resourceWrapper.BankAccountConditionListViewHeader;
            RefreshGridview();
        }

        /// <summary>
        /// Returns selected item in GridView
        /// </summary>
        /// <returns>GridView selected item</returns>
        public Account GetSelectedItem()
        {
            return (Account)AccountGridView.SelectedItem;
        }

        /// <summary>
        /// Refreshing GridView Items
        /// </summary>
        public void RefreshGridview()
        {
            var proxy = new Proxy.Proxy();
            AccountGridView.ItemsSource = proxy.GetUserAccounts(Windows.MainWindow.Credentials);
            AccountGridView.Items.Refresh();
        }
    }
}
