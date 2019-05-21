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
            lblStockNum.Content = "下載中...";
            var stockNos = await StockSyncHelper.SyncStockBase();
            lblStockNum.Content = $"全部股票: {stockNos.Count}";
        }

    }
}
