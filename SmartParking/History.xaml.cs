using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;

using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using System.Diagnostics;
using SQLite;

namespace SmartParking
{

    public partial class History : PhoneApplicationPage{

        string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        public History()
        {

            InitializeComponent();     

        }


        public void AddDb(string fl, string zo, double la, double lo)
        {
            string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            using (var db = new SQLiteConnection(dbPath))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(new historyTableSQlite()
                    {
                        Date = DateTime.Today.ToShortDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        Floor = fl,
                        Zone = zo,
                        longtitude = la,
                        latitude = lo
                    });
                    Debug.WriteLine(db);
                });
                
            }
           




        }
    }
}