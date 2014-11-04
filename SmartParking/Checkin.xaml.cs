using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Networking.Proximity;
using NdefLibrary.Ndef;
using NdefLibraryWp.Ndef;
using Windows.Networking.Sockets; // needed for DataReader, DataWriter
using Windows.UI.Popups;
using Microsoft.Phone.UserData;
using System.Text;
using Windows.Phone.PersonalInformation;
using SmartParking.Resources;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace SmartParking
{

    public partial class Checkin : PhoneApplicationPage
    {
        private ProximityDevice _device;
        private long _subscriptionIdNdef;

        public static double Latitude_do { get; set; }
        public static double Longtitude_do { get; set; }
        public static string Floor_st { get; set; }
        public static string Zone_st { get; set; }
        public static string a { get; set; }
        public static string b { get; set; }

        DbHelper DB_helper = new DbHelper();
        
        

        public Checkin()
        {
            InitializeProximityDevice();
            InitializeComponent();
            Abcd();
        }

        private void SetLogStatus(string newStatus)
        {
            Dispatcher.BeginInvoke(() => { if (LogStatus != null) LogStatus.Text = newStatus; });
        }

        private void SetFloorStatus(string newStatus)
        {
            Dispatcher.BeginInvoke(() => { if (FloorStatus != null) FloorStatus.Text = newStatus; });
            
        }



        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(" ");
        }


        private void InitializeProximityDevice()
        {
            _device = Windows.Networking.Proximity.ProximityDevice.GetDefault();
            if (_device != null)
            {
                _subscriptionIdNdef = _device.SubscribeForMessage("NDEF", MessageReceivedHandler);

            }

        }

        private void Abcd()
        {
            String txt = "my|name|is|Vicky";
            String[] test = txt.Split('|');
            a = test[0];
            b = test[1];
            Debug.WriteLine(a);
            Debug.WriteLine(b);

           

        }


        private void MessageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {
            var rawMsg = message.Data.ToArray();
            var ndefMessage = NdefMessage.FromByteArray(rawMsg);

         
            ////// Loop over all records contained in the NDEF message
            foreach (NdefRecord record in ndefMessage)
            {

                if (NdefTextRecord.IsRecordType(record))
                {
                    // Convert and extract URI info
                    var textRecord = new NdefTextRecord(record);
                    string[] str = textRecord.Text.Split('|');

                    var floors = str[0];
                    Debug.WriteLine(Floor_st);
                    var zones = str[1];
                    Debug.WriteLine(Zone_st);
                    var latitude = str[2];
                    var longtitude = str[3];

                    Floor_st = floors;
                    Zone_st = zones;
                    Latitude_do = double.Parse(latitude);
                    Longtitude_do = double.Parse(longtitude);
                    
                    
                    
                    SetLogStatus("Floor: " + Floor_st + " Zone: " + Zone_st );
                    SetFloorStatus("Latitude: " + latitude + "   Longitude: " + longtitude);
                    //store.AddDb(Floor_st, Zone_st, Latitud_do, Longtitude_do);
                   // DB_helper.AddInfo(Floor_st, Zone_st, Latitude_do, Longtitude_do);
                  
                }
            }
          }
     }
}




