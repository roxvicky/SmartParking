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
        //private long _publishingMessageId;
        


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
                _subscriptionIdNdef = _device.SubscribeForMessage("WindowsMime", MessageReceivedHandler);

            }
            
        }


      
        // Write the message to the tag

        private void SubscribeNdef(object sender, RoutedEventArgs e)
        {
            // Only subscribe for messages if no NDEF subscription is already active
            if (_subscriptionIdNdef != 0) return;
            // Ask the proximity device to inform us about any kind of NDEF message received from
            // another device or tag.
            // Store the subscription ID so that we can cancel it later.ll
            _subscriptionIdNdef = _device.SubscribeForMessage("WindowsMime", MessageReceivedHandler);

        }


        private  void MessageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {


            var buffer = message.Data.ToArray();
            int mimesize = 0;
            //search first '\0' character
            for (mimesize = 0; mimesize < 256 && buffer[mimesize] != 0; ++mimesize)
            {
            };

            //extract mimetype
            var messageType = Encoding.UTF8.GetString(buffer, 0, mimesize);

            //convert data to string. This depends on mimetype value.
            var scanned_message = Encoding.UTF8.GetString(buffer, 256, buffer.Length - 256);

            Dispatcher.BeginInvoke(() =>
            {
                if (_device != null)
                {
                   _device.StopSubscribingForMessage( _subscriptionIdNdef);
                    LogStatus.Text ="Floor:" + scanned_message;

                }
            });

          //  var rawMsg = message.Data.ToArray();
            //var ndefMessage = NdefMessage.FromByteArray(rawMsg);
    

    // Loop over all records contained in the NDEF message
  //  foreach (NdefRecord record in ndefMessage)
    //{
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
        //if (NdefTextRecord.IsRecordType(record))
        //{
        //    // Convert and extract URI info
        //    var textRecord = new NdefTextRecord(record);
        //    SetLogStatus("FLOOR " + textRecord.Text);
            

        //}




   // }
              
}



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




