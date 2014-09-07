using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace SmartParking
{
    public class HistoryDataContext:DataContext
    {
        public HistoryDataContext(string connectionPath)
            : base(connectionPath)
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

