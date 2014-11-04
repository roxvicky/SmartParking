using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking
{
    public class historyTableSQlite
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Floor { get; set; }

        public string Zone { get; set; }

        public double latitude { get; set; }

        public double longtitude { get; set; }

        public string isMarkPoint { get; set; }
    }

}