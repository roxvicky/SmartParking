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

    public partial class History : PhoneApplicationPage
    {
        ObservableCollection<historyTableSQlite> DB_HistoryList = new ObservableCollection<historyTableSQlite>();
        DbHelper Db_helper = new DbHelper();
        int Selected_HistoryId;
        




        // string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
        
        public History()
        {

            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            Db_helper.AddInfo();
            ReadHistoryList_Loaded();
            
            //Debug.WriteLine(Checkin.Floor_st);
            //Debug.WriteLine(Checkin.Zone_st);
            //Debug.WriteLine(Checkin.Latitude_do);
            //Debug.WriteLine(Checkin.Longtitude_do);

          // Selected_HistoryId = int.Parse(NavigationContext.QueryString["SelectedHistoryID"]);
        }

        public void ReadHistoryList_Loaded()
        {
            ReadAllContactsList dbhistory = new ReadAllContactsList();
            DB_HistoryList = dbhistory.GetAllHistory();//Get all DB contacts
            ListData.ItemsSource = DB_HistoryList.OrderByDescending(i => i.Id).ToList();
            
            //Latest contact ID can Display first
            
        }

        public void ListData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListData.SelectedIndex != -1)
            {
                historyTableSQlite listitem = ListData.SelectedItem as historyTableSQlite;
                 Selected_HistoryId = listitem.Id;
            }
            
            
        
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Db_helper.DeleteContact(Selected_HistoryId);
        }

        private void DeleteAll_Click(object sender, EventArgs e)
        {
            DbHelper Db_helper = new DbHelper();
            Db_helper.DeleteAllContact();//delete all db 
            DB_HistoryList.Clear();
            ListData.ItemsSource = DB_HistoryList;

        }

        


        

        //public void updateDB(string fl,string zo,double la, double lo)
        //{

        //    using (var db = new SQLiteConnection(dbPath))
        //    {
        //        var existing = db.Query<historyTableSQlite>("select * from historyTableSQlite").FirstOrDefault();
        //        if (existing != null)
        //        {
        //            existing.Floor = fl;
        //            existing.Zone = zo;
        //            existing.latitude = la;
        //            existing.longtitude = lo;
        //            db.RunInTransaction(() =>
        //            {
        //                db.Update(existing);
        //            });
        //        }




        //    }


        //}

        //public void AddDb(string fl, string zo, double la, double lo)
        //{
        //    string dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
        //    using (var db = new SQLiteConnection(dbPath))
        //    {
        //        db.RunInTransaction(() =>
        //        {
        //            db.Insert(new historyTableSQlite()
        //            {
        //                Date = DateTime.Today.ToShortDateString(),
        //                Time = DateTime.Now.ToShortTimeString(),
        //                Floor = fl,
        //                Zone = zo,
        //                longtitude = la,
        //                latitude = lo
        //            });
        //            Debug.WriteLine(db);
        //        });

        //    }





    }

}