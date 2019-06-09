using System;
using System.Collections.Generic;
using System.Linq;
using StockDbContext;
using StockDbContext.Model;

namespace StockMiner.Helper
{
    internal class DbHelper
    {

        internal static bool AddOrUpdateStockBases(List<StockBase> stockBases)
        {
            using (var db = new StockDbModel())
            {
                AddStockBases(stockBases, db);
                EditDisableStockBases(stockBases, db);

                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch { }
            }
            return false;
        }

        internal static List<StockBase> ReadStockBases()
        {
            throw new NotImplementedException();
        }

        private static void AddStockBases(List<StockBase> stockBases, StockDbModel db)
        {
            foreach (var stockBase in stockBases)
            {
                var target = db.StockBase.SingleOrDefault(s => s.No == stockBase.No);

                if (target == null)
                {
                    db.StockBase.Add(stockBase);
                }
            }
        }

        private static void EditDisableStockBases(List<StockBase> stockBases, StockDbModel db)
        {
            var extra = db.StockBase.Except(stockBases);

            foreach (var item in extra)
            {
                item.Enabled = false;
            }
        }

    }
}