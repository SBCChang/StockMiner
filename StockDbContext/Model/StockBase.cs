using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockDbContext.Model
{
    public class StockBase
    {

        public int No { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ListedDate { get; set; }

        public string Industry { get; set; }

        public bool Enabled { get; set; }

    }
}
