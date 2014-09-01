﻿using System;
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
using Windows.Networking.Sockets;
using Windows.UI.Popups;
using Microsoft.Phone.UserData;
using System.Text;
using Windows.Phone.PersonalInformation;
using SmartParking.Resources;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;



namespace SmartParking
{
    public partial class eTags : PhoneApplicationPage
    {

        private ProximityDevice _device;
        //private long _subscriptionIdNdef;
        private long _publishingMessageId;

        public eTags()
        {
            InitializeComponent();
            _device = Windows.Networking.Proximity.ProximityDevice.GetDefault();

            if (_device != null)
            {
                _device.DeviceArrived += ProximityDeviceArrived;
                _device.DeviceDeparted += ProximityDeviceDeparted;
            }
            else
            {
               StatusOutput.Text += "Failed to initialize proximity device.\n";
            }
            
        }

      


        public void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
          
            var spRecord = new NdefTextRecord{Text = "Floor 3 Zone A" , LanguageCode = "en" };
            var msg = new NdefMessage { spRecord };
            PublishRecord(spRecord, true);
        }
       

        private void SetStatusOutput(string newStatus)
        {

            Dispatcher.BeginInvoke(() => { if (StatusOutput != null) StatusOutput.Text = newStatus; } );
        }
        private void StopPublishingMessage(bool writeToStatusOutput)
        {
            if (_publishingMessageId != 0 && _device != null)
            {
                // Stop publishing the message
                _device.StopPublishingMessage(_publishingMessageId);
                _publishingMessageId = 0;
               // Update status text for UI - only if activated
                if (writeToStatusOutput) SetStatusOutput(AppResources.StatusPublicationStopped);
            }
        }
        private Windows.UI.Core.CoreDispatcher _dispatcher =
     Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

        private async void ProximityDeviceArrived(object sender)
        {
            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                StatusOutput.Text += "Proximate device arrived.\n";
            });
        }

        private async void ProximityDeviceDeparted(object sender)
        {
            await _dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                StatusOutput.Text += "Proximate device departed.\n";
            });
        }

        private void PublishRecord(NdefRecord spRecord, bool writeToTag)
        {
            if (_device == null) return;
            // Make sure we're not already publishing another message
            StopPublishingMessage(false);
           // var spRecord = new NdefTextRecord { Text = "Floor 3 Zone A", LanguageCode = "en" };
            // Wrap the NDEF record into an NDEF message
            var msg = new NdefMessage { spRecord };
            // Convert the NDEF message to a byte array
  //          var msg = NdefMessage.ToByteArray();
            // Publish the NDEF message to a tag or to another device, depending on the writeToTag parameter
            // Save the publication ID so that we can cancel publication later
            _publishingMessageId = _device.PublishBinaryMessage(("NDEF:WriteTag"), msg.ToByteArray().AsBuffer(), MessageWrittenHandler);
            // Update status text for UI
            SetStatusOutput(string.Format((writeToTag ? AppResources.StatusWriteToTag : AppResources.StatusWriteToDevice),  _publishingMessageId));

        }
        private void MessageWrittenHandler(ProximityDevice sender, long messageId)
        {
            // Message was written successfully - inform the user
            //Dispatcher.BeginInvoke(() => MessageBox.Show("NFC Message written"));
            StopPublishingMessage(false);

            //Update status text for UI
            SetStatusOutput(AppResources.StatusMessageWritten);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
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
                //Where da fuck location
                LatitudeTextBlock.Text = geoposition.Coordinate.Latitude.ToString("0.000000");
                LongitudeTextBlock.Text = geoposition.Coordinate.Longitude.ToString("0.000000");
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


    }
}