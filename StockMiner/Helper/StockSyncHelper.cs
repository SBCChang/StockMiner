using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMiner.Helper
{
    internal class StockSyncHelper
    {

        internal async static Task<List<string>> SyncStockNo()
        {
            var result = new List<string>();
            var crawler = new WebCrawler("http://isin.twse.com.tw/isin/C_public.jsp?strMode=2", "/body[1]/table[2]/tr");
            var nodes = await crawler.GetMultiNodes();

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var item = node.InnerText.Trim().Replace("&nbsp;", "");

                    int stockNo;
                    if (item.Length > 4 && int.TryParse(item.Substring(0, 4), out stockNo) && item.Contains("ESVUFR"))
                    {
                        result.Add(stockNo.ToString());
                    }
                }
            }
            return result;
        }

    }
}
