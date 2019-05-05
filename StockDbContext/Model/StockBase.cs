using System;

namespace StockDbContext.Model
{
    public class StockBase
    {

        public string StockNo { get; set; }

        public string StockName { get; set; }

        public DateTime ListedDate { get; set; }

        public string Industry { get; set; }

        public bool Enabled { get; set; }

    }
}
