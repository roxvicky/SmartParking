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
using Windows.Networking.Sockets; // needed for DataReader, DataWriter
using Windows.UI.Popups;
using Microsoft.Phone.UserData;
using System.Text;
using Windows.Phone.PersonalInformation;
using SmartParking.Resources;
using System.Diagnostics;


<<<<<<< HEAD
namespace SmartParking
{
=======


namespace SmartParking
{
       

>>>>>>> parent of 7526fbb... remove second record

    public partial class Checkin : PhoneApplicationPage
    {
        private ProximityDevice _device;
        private long _subscriptionIdNdef;
<<<<<<< HEAD

        public static double Latitud_do { get; set; }
        public static double Longtitude_do { get; set; }
        public static string latitude { get; set; }
        public static string longtitude { get; set; }
        public static string Floor_st { get; set; }
        public static string Zone_st { get; set; }

        History store = new History();
        public Checkin()
        {
            InitializeProximityDevice();
            InitializeComponent();

        }

        private void SetLogStatus(string newStatus)
        {
            Dispatcher.BeginInvoke(() => { if (LogStatus != null) LogStatus.Text = newStatus; });
        }

        private void SetFloorStatus(string newStatus)
        {
            Dispatcher.BeginInvoke(() => { if (FloorStatus != null) FloorStatus.Text = newStatus; });
<<<<<<< HEAD
            
=======
        private long _publishingMessageId;
        


        public Checkin()
        {
            InitializeComponent();
            InitializeProximityDevice();

        }

        

        private void SetBoolStatus(string newStatus)
        {
            // Update the status output UI element in the UI thread
            // (some of the callbacks are in a different thread that wouldn't be allowed
            // to modify the UI thread)
            Dispatcher.BeginInvoke(() => { if (BoolStatus != null) BoolStatus.Text = newStatus; });
        }

        private void SetLogStatus(string newStatus)
        {
            Dispatcher.BeginInvoke(() => { if (LogStatus != null) LogStatus.Text = newStatus; });
>>>>>>> parent of 7526fbb... remove second record
=======

>>>>>>> origin/master
        }



<<<<<<< HEAD
        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(" ");
=======
        
         private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("The green activated symbol describes that your check-in is successful");
            // TODO: Add event handler implementation here.
>>>>>>> parent of 7526fbb... remove second record
        }


        private void InitializeProximityDevice()
        {
            _device = Windows.Networking.Proximity.ProximityDevice.GetDefault();
            if (_device != null)
            {
                _subscriptionIdNdef = _device.SubscribeForMessage("NDEF", MessageReceivedHandler);

            }
<<<<<<< HEAD

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

                    var Floor_st = str[0];
                    var Zone_st = str[1];
                    var latitude = str[2];
                    var longtitude = str[3];
                    Latitud_do = double.Parse(latitude);
                    Longtitude_do = double.Parse(longtitude);



                    SetLogStatus("Floor: " + Floor_st + " Zone: " + Zone_st);
                    SetFloorStatus("Latitude: " + latitude + "   Longitude: " + longtitude);
                    store.AddDb(Floor_st, Zone_st, Latitud_do, Longtitude_do);

                }
            }
<<<<<<< HEAD
          }
     }
=======
            
        }


      
        // Write the message to the tag

        private void SubscribeNdef(object sender, RoutedEventArgs e)
        {
            // Only subscribe for messages if no NDEF subscription is already active
            if (_subscriptionIdNdef != 0) return;
            // Ask the proximity device to inform us about any kind of NDEF message received from
            // another device or tag.
            // Store the subscription ID so that we can cancel it later.ll
            _subscriptionIdNdef = _device.SubscribeForMessage("NDEF", MessageReceivedHandler);

        }


        private async void MessageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {
            var rawMsg = message.Data.ToArray();
            var ndefMessage = NdefMessage.FromByteArray(rawMsg);
    

    // Loop over all records contained in the NDEF message
    foreach (NdefRecord record in ndefMessage)
    {
        //Debug.WriteLine("Record type: " + Encoding.UTF8.GetString(record.Type, 0, record.Type.Length));
        // Check the type of each record - handling a Smart Poster, URI and Text record in this example
        //var specializedType = record.CheckSpecializedType(false);
        
        //if (specializedType == typeof(NdefTextRecord))
        //{
        //    // Convert and extract Text record info
        //    var textRecord = new NdefTextRecord(record);
        //    Debug.WriteLine("Text: " + textRecord.Text);
        //    Debug.WriteLine("Language code: " + textRecord.LanguageCode);
        //    var textEncoding = (textRecord.TextEncoding == NdefTextRecord.TextEncodingType.Utf8 ? "UTF-8" : "UTF-16");
        //    Debug.WriteLine("Encoding: " + textEncoding);
        //    SetLogStatus(textRecord.Text );
        //}
        //var rawMsg = message.Data.ToArray();
        //var ndefMessage = NdefMessage.FromByteArray(rawMsg);
        if (NdefTextRecord.IsRecordType(record))
        {
            // Convert and extract URI info
            var textRecord = new NdefTextRecord(record);
            SetLogStatus("FLOOR " + textRecord.Text);
            

        }




    }
              
>>>>>>> parent of 7526fbb... remove second record
=======
        }
    }
>>>>>>> origin/master
}



<<<<<<< HEAD
<<<<<<< HEAD
=======
           //SetLogStatus(string.Format(AppResources.StatusTagParsed, tagContents));
        }



       /* private string ConvertTypeNameFormatToString(NdefRecord.TypeNameFormatType tnf)
        {
            // Each record contains a type name format, which defines which format
            // the type name is actually in.
            // This method converts the constant to a human-readable string.
            string tnfString;
            switch (tnf)
            {
                case NdefRecord.TypeNameFormatType.Empty:
                    tnfString = "Empty NDEF record (does not contain a payload)";
                    break;
                case NdefRecord.TypeNameFormatType.NfcRtd:
                    tnfString = "NFC RTD Specification";
                    break;
                case NdefRecord.TypeNameFormatType.Mime:
                    tnfString = "RFC 2046 (Mime)";
                    break;
                case NdefRecord.TypeNameFormatType.Uri:
                    tnfString = "RFC 3986 (Url)";
                    break;
                case NdefRecord.TypeNameFormatType.ExternalRtd:
                    tnfString = "External type name";
                    break;
                case NdefRecord.TypeNameFormatType.Unknown:
                    tnfString = "Unknown record type; should be treated similar to content with MIME type 'application/octet-stream' without further context";
                    break;
                case NdefRecord.TypeNameFormatType.Unchanged:
                    tnfString = "Unchanged (partial record)";
                    break;
                case NdefRecord.TypeNameFormatType.Reserved:
                    tnfString = "Reserved";
                    break;
                default:
                    tnfString = "Unknown";
                    break;
            }
            return tnfString;
        }
    */
          
    }



>>>>>>> parent of 7526fbb... remove second record

=======
>>>>>>> origin/master
