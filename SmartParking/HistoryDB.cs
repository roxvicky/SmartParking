using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;   
using System.Data.Linq.Mapping;  


namespace SmartParking
{
    [Table]
    public class HistoryDB
    {
        //[Column
        //(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        //public int id
        //{ get; set; }

        [Column(CanBeNull = false)]
        public DateTime Date
        { get; set; }

        [Column(CanBeNull = false)]
        public TimeSpan Time
        { get; set; }

        [Column(CanBeNull = false)]
        public String Zone
        { get; set; }

        [Column(CanBeNull = false)]
        public String Floor
        { get; set; }

        [Column(CanBeNull = false)]
        public double location_latitude
        { get; set; }

        [Column(CanBeNull = false)]
        public double location_longtitud
        { get; set; }

    }
}

