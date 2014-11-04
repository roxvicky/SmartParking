using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using Windows.Devices.Geolocation;

namespace SmartParking

{
    public partial class Findcar : PhoneApplicationPage
    {
        private static string baseUri = "bingmaps:?";

        public Findcar()
        {
       
            InitializeComponent();
            //Floor_fc.Text = "Floor :" + Checkin.Floor_st;
            //Console.WriteLine(Floor_fc.Text);
            //Zone_fc.Text = "Zone :" + Checkin.Zone_st;
            
              
           
              
        }

        public static void LaunchMap(double latitude, double longitude, int zoom)
        {
            string uri = string.Format("{0}cp={1:N5}~{2:N5}&lvl={3}", baseUri, latitude, longitude, zoom);

            Launch(new Uri(uri));
        }

        
        private static async void Launch(Uri uri)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

        }

        private void Map_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            LaunchMap(Checkin.Latitude_do, Checkin.Longtitude_do, 20);
            // TODO: Add event handler implementation here.
        }

        

   
        

    }
}