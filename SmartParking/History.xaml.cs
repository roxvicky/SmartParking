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





namespace SmartParking
{

    public partial class History : PhoneApplicationPage ,INotifyPropertyChanged
    {
        private HistoryDataContext ToDoHistoryLog;
        private String z;
        private String f;
        private Double lt;
        private Double lg;

        private ObservableCollection<HistoryDB> _ToDoHistory;
        public ObservableCollection<HistoryDB> HistoryItems
        {
            get
            {
                return _ToDoHistory;
            }
            set
            {
                if(_ToDoHistory != value){
                    _ToDoHistory = value;
                    NotifyPropertyChanged("HistoryDbs");
                }
            }
        }
        public History()
        {
            
            InitializeComponent();

           // debug();
           createDB();
            
        }

        

        public void createDB()
        {
            //Debug.WriteLine(z);
            ToDoHistoryLog = new HistoryDataContext(HistoryDataContext.DBConnectionString);
            this.DataContext = this;
            var HistoryInDB = from HistoryDB todo in ToDoHistoryLog.ToDoHistory select todo;


            //HistoryItems = new ObservableCollection<HistoryDB>();
            HistoryItems = new ObservableCollection<HistoryDB>(HistoryInDB);
            
            //  Debug.WriteLine(HistoryItems);
            ListData.ItemsSource = HistoryItems;
            AddDb();
            //base.OnNavigatedFrom(e);

        }

        public void AddDb()
        {
            HistoryDB a = new HistoryDB
            {

                //Date = DateTime.Now.ToShortDateString(),
                //Time = DateTime.Now.ToString("HH:mm ss tt"),
                //Zone =  Checkin.Zone_st,
                //Floor = Checkin.Floor_st,
                //location_latitude = Checkin.Latitud_do,
                //location_longtitude = Checkin.Longtitude_do
                
            };
            HistoryItems.Add(a);
            ToDoHistoryLog.ToDoHistory.InsertOnSubmit(a);
            ToDoHistoryLog.SubmitChanges();
            //GetHistoryLog();
        }

        //public IList<HistoryDB> GetHistoryLog()
        //{
        //    IList<HistoryDB> HistoryList = null;
        //    using (HistoryDataContext historylog = new HistoryDataContext(HistoryDataContext.DBConnectionString))
        //    {
        //        IQueryable<HistoryDB> query = from history in historylog.ToDoHistory select history;
        //        HistoryList = query.ToList();
        //    }
        //    return HistoryList;
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void setZone(String zone)
        {
            this.z = zone;
        }

        public void setFloor(String floor)
        {
            this.f = floor;
        }

        public void setLatitute(Double lt)
        {
            this.lt = lt;
        }

        public void setLongtitute(Double lg)
        {
            this.lg = lg;
        }

        public void debug()
        {
            Debug.WriteLine("Zone: " + z); 
        }
    }
}

    



