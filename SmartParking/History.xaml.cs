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



namespace SmartParking
{


    public partial class History : PhoneApplicationPage
    {

        public History()
        {
            InitializeComponent();

           /* using (HistoryDataContext historylog = new HistoryDataContext(HistoryDataContext.DBConnectionString))
            {
                if (historylog.DatabaseExists() == false)
                {
                    historylog.CreateDatabase();
                }
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

            IList<HistoryDB> HistoryLog = this.GetHistoryLog();
            StringBuilder details = new StringBuilder();

            foreach (HistoryDB std in HistoryLog)
            {
                details.AppendLine("Date:" + std.Date + "Time:" + std.Time + "Floor:" + std.Floor + "Zone:" + std.Zone +
                "Longtitude:" + std.location_longtitud + "Latitude:" + std.location_latitude);
            }

            IQueryable<HistoryDB> query = from history in HistoryLog where history.Date == selectedName select update;
            StudentDetails updatevalue = query.FirstOrDefault();
            updatevalue.No = Convert.ToInt16(notextbox.Text);
            updatevalue.Address = addresstxtbox.Text;
            studentdb.SubmitChanges();


            //test
            //MessageBox.Show(details.ToString());   

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
            }*/
        }
    }
}


