using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq.Mapping;
using System.Data.Linq;


namespace SmartParking
{
    [Table]
    public class HistoryDB 
    {

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

