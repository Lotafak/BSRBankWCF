using System.Windows.Controls;
using Client.Windows;

namespace Client.Pages
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        private readonly ResourceWrapper _resourceWrapper = new ResourceWrapper();

        public HistoryPage()
        {
            InitializeComponent();

            // Initialize headers column name
            HistoryOperationType.Header = _resourceWrapper.HistoryOperationTypeListViewHeader;
            HistoryFrom.Header = _resourceWrapper.HistoryFromListViewHeader;
            HistoryTo.Header = _resourceWrapper.HistoryToListViewHeader;
            HistoryAmount.Header = _resourceWrapper.HistoryAmountListViewHeader;
            HistoryDate.Header = _resourceWrapper.HistoryDateListViewHeader;

            // Fill History ListView 
            RefreshHistoryList();
        }

        private void RefreshHistoryList()
        {
            var proxy = new Proxy.Proxy();
            HistoryListView.ItemsSource = proxy.GetUsersHistory(MainWindow.Credentials);
            HistoryListView.Items.Refresh();
        }
    }
}
