using System.Windows;
using StockMiner.Helper;

namespace StockMiner
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnSyncStockNo_Click(object sender, RoutedEventArgs e)
        {
            lblStockNumber.Content = "同步中";
            var result = await StockSyncHelper.SyncStockBase();
            if (result)
            {
                var stocks = DbHelper.ReadStockBases();
                lblStockNumber.Content = $"全部股票: {stocks.Count}";
            }
            else
            {
                lblStockNumber.Content = "同步失敗";
            }
        }

    }
}
