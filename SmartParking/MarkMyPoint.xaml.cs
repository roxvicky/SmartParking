using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;

namespace SmartParking
{
    public partial class MarkMyPoint : PhoneApplicationPage
    {
        private double latitude_pv {get; set;}
        private double longtitude_pv { get; set; }
        public MarkMyPoint()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                // User has opted in or out of Location
                return;
            }
            else
            {
                MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }


        private async void OneShotLocation_Click(object sender, RoutedEventArgs e)
        {

            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            {
                // The user has opted out of Location.
                return;
            }

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                Latitude.Text = "Latitude :"+geoposition.Coordinate.Latitude.ToString("0.00000000");
                Longtitude.Text = "Longtitude :" + geoposition.Coordinate.Longitude.ToString("0.00000000");
                latitude_pv = geoposition.Coordinate.Latitude;
                longtitude_pv = geoposition.Coordinate.Longitude;

                Map MyMap = new Map();
                MyMap.Center = new GeoCoordinate(latitude_pv, longtitude_pv);
                ContentPanel.Children.Add(MyMap);
                MyMap.ZoomLevel = 17;
                MyMap.Height = 400;
                //MyMap.ColorMode = MapColorMode.Dark;
              
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    StatusTextBlock.Text = "location  is disabled in phone settings.";
                }
                //else
                {
                    // something else happened acquring the location
                }
            }
        }

        private void MyMap_CenterChanged(object sender, MapCenterChangedEventArgs e)
        {

        }

       /* private void MyMap_CenterChanged(object sender, MapCenterChangedEventArgs e)
        {
            Map MyMap = new Map();
            MyMap.Center = new GeoCoordinate(latitude_pv, longtitude_pv);
            ContentPanel.Children.Add(MyMap);
            MyMap.ZoomLevel = 10;

        }*/

    }
}