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




namespace SmartParking
{



    public partial class Checkin : PhoneApplicationPage
    {
        private ProximityDevice _device;
        private long _subscriptionIdNdef;
        private long _publishingMessageId;



        public Checkin()
        {
            InitializeComponent();
            InitializeProximityDevice();

        }





        private void SetLogStatus(string newStatus)
        {
            Dispatcher.BeginInvoke(() => { if (LogStatus != null) LogStatus.Text = newStatus; });
        }




        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("The green activated symbol describes that your check-in is successful");
            // TODO: Add event handler implementation here.
        }


        private void InitializeProximityDevice()
        {
            _device = Windows.Networking.Proximity.ProximityDevice.GetDefault();
            if (_device != null)
            {
                _subscriptionIdNdef = _device.SubscribeForMessage("NDEF", MessageReceivedHandler);

            }

        }



        // Write the message to the tag

        //private void SubscribeNdef(object sender, RoutedEventArgs e)
        //{
        //    // Only subscribe for messages if no NDEF subscription is already active
        //    if (_subscriptionIdNdef != 0) return;
        //    // Ask the proximity device to inform us about any kind of NDEF message received from
        //    // another device or tag.
        //    // Store the subscription ID so that we can cancel it later.ll
        //    _subscriptionIdNdef = _device.SubscribeForMessage("WindowsMime", MessageReceivedHandler);

        //}


        private void MessageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {
        //    var buffer = message.Data.ToArray();
        //    int mimesize = 0;
        //    //search first '\0' charactere
        //    for (mimesize = 0; mimesize < 256 && buffer[mimesize] != 0; ++mimesize)
        //    {
        //    };

        //    //extract mimetype
        //    var mimeType = Encoding.UTF8.GetString(buffer, 0, mimesize);

        //    //convert data to string. This traitement depend on mimetype value.
        //    var data = Encoding.UTF8.GetString(buffer, 256, buffer.Length - 256);


            var rawMsg = message.Data.ToArray();
            var ndefMessage = NdefMessage.FromByteArray(rawMsg);

         
            ////// Loop over all records contained in the NDEF message
            foreach (NdefRecord record in ndefMessage)
            {

                if (NdefTextRecord.IsRecordType(record))
                {
                    // Convert and extract URI info
                    var textRecord = new NdefTextRecord(record);
                    //var str = textRecord.Text;
                    string[] str = textRecord.Text.Split(' ');
                    var latitude = str[2];
                    var longtitude = str[3];                    
                    SetLogStatus("Floor: " + str[0] + " Zone: " + str[1] + " Latitude: " + latitude + " Longtitude: " + longtitude);

                }
            }
          }
     }
}




