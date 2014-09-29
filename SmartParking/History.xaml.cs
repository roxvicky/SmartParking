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
        private string floor_db { get; set; }
        private string zone_db { get; set; }
        private double latitude_db { get; set; }
        private double longtitude_db { get; set; }

        public History()
        {

            InitializeComponent();

        }

        public void updateDB()
        {

            using (var db = new SQLiteConnection(dbPath))
            {
                var existing = db.Query<historyTableSQlite>("select * from historyTableSQlite").FirstOrDefault();
                if (existing != null)
                {
                    existing.Floor = floor_db;
                    existing.Zone = zone_db;
                    existing.latitude = latitude_db;
                    existing.longtitude = longtitude_db;
                    db.RunInTransaction(() =>
                    {
                        db.Update(existing);
                    });
                }
            }


        }



        public void AddDb()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(new historyTableSQlite()
                    {
                        Date = DateTime.Today.ToShortDateString(),
                        Time = DateTime.Now.ToShortTimeString(),
                        Floor = floor_db,
                        Zone = zone_db,
                        longtitude = longtitude_db,
                        latitude = latitude_db
                    });
                });
            }




        }
    }
}