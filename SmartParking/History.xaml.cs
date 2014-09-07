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
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;



namespace SmartParking
{


    public partial class History : PhoneApplicationPage
    {

        public History()
        {
            InitializeComponent();

            using (HistoryDataContext historylog = new HistoryDataContext(HistoryDataContext.DBConnectionString))
            {
                if (historylog.DatabaseExists() == false)
                {
                    historylog.CreateDatabase();
                }
                HistoryDB hdb = new HistoryDB{
                    Date = DateTime.Today,
                    Time = DateTime.Now.TimeOfDay,
                    Zone = Checkin.Zone_st,
                    Floor = Checkin.Floor_st,
                    location_latitude = Checkin.Latitud_do,
                    location_longtitud = Checkin.Longtitude_do
                };
                historylog.history.InsertOnSubmit(hdb);
                historylog.SubmitChanges();                
            }
        }

        public IList<HistoryDB> GetHistoryLog()   
            {
                IList<HistoryDB> HistoryList = null;
                using (HistoryDataContext historylog = new HistoryDataContext(HistoryDataContext.DBConnectionString))   
                {   
                   IQueryable<HistoryDB> query = from history in historylog.history select history;
                   HistoryList = query.ToList();   
                }
                return HistoryList;   
            }  






    }
}