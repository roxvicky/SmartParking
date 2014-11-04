using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartParking;

namespace SmartParking
{
    public class ReadAllContactsList
    {
        DbHelper Db_Helper = new DbHelper();
        public ObservableCollection<historyTableSQlite> GetAllHistory()
        {
            return Db_Helper.ReadHistory();
        }
    }
}