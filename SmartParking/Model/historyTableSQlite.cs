using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartParking
{
    public class historyTableSQlite : INotifyPropertyChanged
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        
        public int Id { 
            get;
            set; 
        }
        private int idValue;

        private string dateValue = string.Empty;

        public string Date {
            get { return this.dateValue; }
            set
            {
                if (value != this.dateValue)
                {
                    this.dateValue = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }


        private string timeValue = string.Empty;
        public string Time
        {
            get { return this.timeValue; }
            set
            {
                if (value != this.timeValue)
                {
                    this.timeValue = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        private string floorValue = string.Empty;
        public string Floor
        {
            get { return this.floorValue; }
            set
            {
                if (value != this.floorValue)
                {
                    this.floorValue = value;
                    NotifyPropertyChanged("Floor");
                }
            }
        }

        public string zoneValue;
        public string Zone
        {
            get { return this.zoneValue; }
            set
            {
                if (value != this.zoneValue)
                {
                    this.zoneValue = value;
                    NotifyPropertyChanged("Zone");
                }
            }
        }

        private double latValue;
        public double latitude
        {
            get { return latValue; }
            set
            {
                if (value != this.latValue)
                {
                    this.latValue = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        private double lonValue;
        public double longtitude
        {
            get { return this.lonValue; }
            set
            {
                if (value != this.lonValue)
                {
                    this.lonValue = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }

       // public string isMarkPoint { get; set; }

        public historyTableSQlite()
        {

        }

        public historyTableSQlite(string date,string time,string floor,string zone,double lat,double lng)
        {
            Date = date;
            Time = time;
            Floor = floor;
            Zone = zone;
            latitude = lat;
            longtitude = lng;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {   
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
    

}