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
using System.Data.Linq;

namespace SmartParking
{

    public class HistoryDataContext:DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/History.sdf";
        public HistoryDataContext(string DBConnectionString)
            : base(DBConnectionString)
        {
    }

       public Table<HistoryDB> history
        {
            get
            {
                return this.GetTable<HistoryDB>();
            }
        }


    }

}

