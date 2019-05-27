using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StockDbContext.Model;
using StockMiner.Enum;

namespace StockMiner.Helper
{
    internal class StockSyncHelper
    {

        internal async static Task<bool> SyncStockBase()
        {
            var result = false;
            var crawler = new WebCrawler("http://isin.twse.com.tw/isin/C_public.jsp?strMode=2", "/body[1]/table[2]/tr");
            var nodes = await crawler.GetMultiNodes();

            if (nodes != null)
            {
                var stockBases = GetStockBases(nodes);
                result = DbHelper.AddOrUpdateStockBases(stockBases);
            }

            return result;
        }

        private static List<StockBase> GetStockBases(HtmlNodeCollection nodes)
        {
            var result = new List<StockBase>();

            for (int row = 0; row < nodes.Count; row++)
            {
                if (nodes[row].ChildNodes.Count > 5 && nodes[row].ChildNodes[5].InnerText.Trim() == "ESVUFR")
                {
                    var stockBase = new StockBase() { Enabled = true };
                    var targetRow = nodes[row];
                    for (int column = 0; column < targetRow.ChildNodes.Count; column++)
                    {
                        UpdateStockBase(targetRow, stockBase, column);
                    }
                    result.Add(stockBase);
                }
            }

            return result;
        }

        private static void UpdateStockBase(HtmlNode targetRow, StockBase stockBase, int column)
        {
            switch (column)
            {
                case (int)StockBasePosition.NoAndName:
                    var noAndName = targetRow.ChildNodes[column].InnerText.Trim().Split('　');
                    int no;
                    if (int.TryParse(noAndName[0], out no))
                    {
                        stockBase.No = no;
                        stockBase.Name = noAndName[1];
                    }
                    break;
                case (int)StockBasePosition.ListedDate:
                    var date = targetRow.ChildNodes[column].InnerText.Trim();
                    DateTime listedDate;
                    if (DateTime.TryParse(date, out listedDate))
                    {
                        stockBase.ListedDate = listedDate;
                    }
                    break;
                case (int)StockBasePosition.Industry:
                    stockBase.Industry = targetRow.ChildNodes[column].InnerText.Trim();
                    break;
                default:
                    break;
            }
        }

    }
}
