using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.ComponentModel;



namespace SmartParking
{
 
    
    [Table]
    public class HistoryDB : INotifyPropertyChanged 
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", AutoSync = AutoSync.OnInsert, CanBeNull = false)]
        public int ID { get; set; }

        private String _datedb;
        [Column(CanBeNull = false)]
        public String Date
        { get { return DateTime.Now.ToShortDateString();} set{ NotifyPropertyChanged("Date"); _datedb = value;} }

        private String _timedb;
        [Column(CanBeNull = false)]
        public String Time
        { get { return DateTime.Now.ToString("HH:mm ss tt");} set{ NotifyPropertyChanged("Time"); _timedb = value;} }

        private String _zonedb;
        [Column(CanBeNull = false)]
        public String Zone
        { get { return _zonedb; } set { NotifyPropertyChanged("Zone"); _zonedb = value; } }

        private String _floordb;
        [Column(CanBeNull = false)]
        public String Floor
        { get { return _floordb; } set { NotifyPropertyChanged("Floor"); _floordb = value; } }

        private Double _latitudedb;
        [Column(CanBeNull = false)]
        public double location_latitude
        { get { return _latitudedb; } set { NotifyPropertyChanged("Latitude"); _latitudedb = value; } }

        private Double _longtitude;
        [Column(CanBeNull = false)]
        public double location_longtitude
        { get { return _longtitude; } set { NotifyPropertyChanged("Longtitude"); _longtitude = value; } }

        [Column(CanBeNull = true)]
        public String isMarkPoint
        { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

       

    }


