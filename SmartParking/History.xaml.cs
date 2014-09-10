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
using System.Data.Linq;


namespace SmartParking
{

    public partial class History : PhoneApplicationPage
    {
        private readonly HistoryDataContext historylog; 
        public History()
        {
            InitializeComponent();

           
        }

        public HistoryDataContext Log
        {
            get { return historylog; }
        }

        public void createDB()
        {
            using (HistoryDataContext historylog = new HistoryDataContext(HistoryDataContext.DBConnectionString))
            {
                if (historylog.DatabaseExists() == false)
                {
                    historylog.CreateDatabase();
                }
            }

        }

        public void addDataDB()
        {
            using (HistoryDataContext historylog = new HistoryDataContext(HistoryDataContext.DBConnectionString))
            {
                HistoryDB hdb = new HistoryDB
                {
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
                IQueryable<HistoryDB> query = from histoy in historylog.history select histoy;
                HistoryList = query.ToList();
            }
            return HistoryList ;
        }

        
    }
}


