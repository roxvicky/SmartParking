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
using SmartParking.Resources;



namespace SmartParking
{
    public class DbHelper
    {


        SQLiteConnection dbConn;
        
        

        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<historyTableSQlite>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //retrieve all list from the database
        public ObservableCollection<historyTableSQlite> ReadHistory()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<historyTableSQlite> myCollection = dbConn.Table<historyTableSQlite>().ToList<historyTableSQlite>();
                ObservableCollection<historyTableSQlite> HistoryList = new ObservableCollection<historyTableSQlite>(myCollection);
                return HistoryList;
            }
        }

        // Insert the new info in the histrorytablesqlite table. 
        public void Insert(historyTableSQlite newcontact)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newcontact);
                });
            }
        }

        public void AddInfo()
        {
            //string f = Checkin.Floor_st;
            Debug.WriteLine(Checkin.a);
            string z = Checkin.Zone_st;
            DbHelper Db_helper = new DbHelper();
            Db_helper.Insert((new historyTableSQlite
            {
                Date = DateTime.Now.ToShortDateString(),
                Time = DateTime.Now.ToShortTimeString(),
                Zone = Checkin.Floor_st,
                Floor = Checkin.Zone_st,
                latitude =Checkin.Latitude_do,
                longtitude = Checkin.Longtitude_do
            }));

        }


       // Delete specific contact
        public void DeleteContact(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingvalue = dbConn.Query<historyTableSQlite>("select * from historyTableSQlite where Id =" + Id).FirstOrDefault();
                if (existingvalue != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingvalue);
                    });
                }
            }
        }

        //Delete all contactlist or delete Contacts table
        public void DeleteAllContact()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                //dbConn.RunInTransaction(() =>
                //   {
                dbConn.DropTable<historyTableSQlite>();
                dbConn.CreateTable<historyTableSQlite>();
                dbConn.Dispose();
                dbConn.Close();
                //});
            }
        }

    }


}
