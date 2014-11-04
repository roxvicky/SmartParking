//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Navigation;
//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
//using System.IO.IsolatedStorage;
//using System.Data.Linq;
//using System.Data.Linq.Mapping;
//using System.ComponentModel;
//using System.Collections.ObjectModel;
//using System.Text;

//using System.IO;
//using System.Threading.Tasks;
//using Windows.Storage;
//using System.Diagnostics;
//using SQLite;

//namespace SmartParking
//{
//    public class DbHelper
//    {

//        SQLiteConnection dbConn;

//        //Create database
//        public async Task<bool> onCreate(string DB_PATH)
//        {
//            try
//            {
//                if (!CheckFileExists(DB_PATH).Result)
//                {
//                    using (dbConn = new SQLiteConnection(DB_PATH))
//                    {
//                        dbConn.CreateTable<historyTableSQlite>();
//                    }
//                }
//                return true;
//            }
//            catch
//            {
//                return false;
//            }


//        }

//        private async Task<bool> CheckFileExists(string fileName)
//        {
//            try
//            {
//                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        public ObservableCollection<historyTableSQlite> ReadHistory()
//        {
//            using (var dbConn = new SQLiteConnection(App.DB_PATH))
//            {
//                List<historyTableSQlite> myCollection = dbConn.Table<historyTableSQlite>().ToList<historyTableSQlite>();
//                ObservableCollection<historyTableSQlite> HistoryList= new ObservableCollection<historyTableSQlite>(myCollection);
//                return HistoryList;
//            }
//        }


//        // Insert the new contact in the Contacts table. 
//        public void Insert(historyTableSQlite newcontact)
//        {
//            using (var dbConn = new SQLiteConnection(App.DB_PATH))
//            {
//                dbConn.RunInTransaction(() =>
//                {
//                    dbConn.Insert(newcontact);
//                });
//            }
//        }

//        private async void AddContact()
//        {
//            DbHelper Db_Help = new DbHelper();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs 
           
//                Db_Help.Insert(new historyTableSQlite(Checkin.Floor_st, Checkin.Zone_st,Checkin.lat));// 
//                NavigationService.Navigate(new Uri("/Views/ReadContactList.xaml", UriKind.Relative));//after add contact redirect to contact listbox page 
            
            
           

           
//        }


//    }


//}
