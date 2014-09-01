using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking.Proximity; // needed for the ProximityDevice

using Windows.Networking.Sockets; // needed for DataReader, DataWriter
using Windows.UI.Popups;
using NdefLibrary.Ndef;
using NdefLibraryWp.Ndef;
using Microsoft.Phone.UserData;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

using Windows.Phone.PersonalInformation;
using SmartParking.Resources;





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

        #region UI management

        private void SetStatusOutput(string newStatus)
        {
            // Update the status output UI element in the UI thread
            // (some of the callbacks are in a different thread that wouldn't be allowed
            // to modify the UI thread)
             // Dispatcher.BeginInvoke(() => { if (StatusOutput != null) StatusOutput.Text = newStatus; });
        }

        private void MessageWrittenHandler(ProximityDevice sender, long messageid)
        {
            // Stop publishing the message
            StopPublishingMessage(false);
            // Update status text for UI
            SetStatusOutput(AppResources.StatusMessageWritten);
        }
        #endregion

        private void InitializeProximityDevice()
        {
            _device = Windows.Networking.Proximity.ProximityDevice.GetDefault();

            if (_device != null)
            {


                MessageBox.Show("Proximity device initialized.\n");
            }
            else
            {
                MessageBox.Show("Failed to initialized proximity device.\n");
            }
        }


        #region Subscribe Ndef
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
            // Get the raw NDEF message data as byte array
            var rawMsg = message.Data.ToArray();
            // Let the NDEF library parse the NDEF message out of the raw byte array
            var ndefMessage = NdefMessage.FromByteArray(rawMsg);

            // Analysis result
            var tagContents = new StringBuilder();

            // Loop over all records contained in the NDEF message
            foreach (NdefRecord record in ndefMessage)
            {
                // --------------------------------------------------------------------------
                // Print generic information about the record
                if (record.Id != null && record.Id.Length > 0)
                {
                    // Record ID (if present)
                    tagContents.AppendFormat("Id: {0}\n", Encoding.UTF8.GetString(record.Id, 0, record.Id.Length));
                }
                // Record type name, as human readable string
                tagContents.AppendFormat("Type name: {0}\n", ConvertTypeNameFormatToString(record.TypeNameFormat));
                // Record type
                if (record.Type != null && record.Type.Length > 0)
                {
                    tagContents.AppendFormat("Record type: {0}\n",
                                             Encoding.UTF8.GetString(record.Type, 0, record.Type.Length));
                }

                // --------------------------------------------------------------------------
                // Check the type of each record
                // Using 'true' as parameter for CheckSpecializedType() also checks for sub-types of records,
                // e.g., it will return the SMS record type if a URI record starts with "sms:"
                // If using 'false', a URI record will always be returned as Uri record and its contents won't be further analyzed
                // Currently recognized sub-types are: SMS, Mailto, Tel, Nokia Accessories, NearSpeak, WpSettings
                var specializedType = record.CheckSpecializedType(true);

                if (specializedType == typeof(NdefMailtoRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract Mailto record info
                    var mailtoRecord = new NdefMailtoRecord(record);
                    tagContents.Append("-> Mailto record\n");
                    tagContents.AppendFormat("Address: {0}\n", mailtoRecord.Address);
                    tagContents.AppendFormat("Subject: {0}\n", mailtoRecord.Subject);
                    tagContents.AppendFormat("Body: {0}\n", mailtoRecord.Body);
                }
                else if (specializedType == typeof(NdefUriRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract URI record info
                    var uriRecord = new NdefUriRecord(record);
                    tagContents.Append("-> URI record\n");
                    tagContents.AppendFormat("URI: {0}\n", uriRecord.Uri);
                }
                else if (specializedType == typeof(NdefLaunchAppRecord))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract LaunchApp record info
                    var launchAppRecord = new NdefLaunchAppRecord(record);
                    tagContents.Append("-> LaunchApp record" + Environment.NewLine);
                    if (!string.IsNullOrEmpty(launchAppRecord.Arguments))
                        tagContents.AppendFormat("Arguments: {0}\n", launchAppRecord.Arguments);
                    if (launchAppRecord.PlatformIds != null)
                    {
                        foreach (var platformIdTuple in launchAppRecord.PlatformIds)
                        {
                            if (platformIdTuple.Key != null)
                                tagContents.AppendFormat("Platform: {0}\n", platformIdTuple.Key);
                            if (platformIdTuple.Value != null)
                                tagContents.AppendFormat("App ID: {0}\n", platformIdTuple.Value);
                        }
                    }
                }
                else if (specializedType == typeof(NdefVcardRecordBase))
                {
                    // --------------------------------------------------------------------------
                    // Convert and extract business card info
                    var vcardRecord = await NdefVcardRecord.CreateFromGenericBaseRecord(record);
                    tagContents.Append("-> Business Card record" + Environment.NewLine);
                    tagContents.AppendFormat("vCard Version: {0}" + Environment.NewLine,
                                             vcardRecord.VCardFormatToWrite == VCardFormat.Version2_1 ? "2.1" : "3.0");
                    var contact = vcardRecord.ContactData;
                    var contactInfo = await contact.GetPropertiesAsync();
                    foreach (var curProperty in contactInfo.OrderBy(i => i.Key))
                    {
                        tagContents.Append(String.Format("{0}: {1}" + Environment.NewLine, curProperty.Key, curProperty.Value));
                    }
                }
                else
                {
                    // Other type, not handled by this demo
                    tagContents.Append("NDEF record not parsed by this demo app" + Environment.NewLine);
                }
            }
            // Update status text for UI
            SetStatusOutput(string.Format(AppResources.StatusTagParsed, tagContents));
        }



        private string ConvertTypeNameFormatToString(NdefRecord.TypeNameFormatType tnf)
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

        #endregion

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("The green activated symbol describes that your check-in is successful");
            // TODO: Add event handler implementation here.
        }

        #region Publish
        private void WriteLaunchApp(object sender, System.Windows.RoutedEventArgs e)
        {
            // Create a new LaunchApp record to launch this app
            // The app will print the arguments when it is launched (see MainPage.OnNavigatedTo() method)
            var record = new NdefLaunchAppRecord { Arguments = "Hello World" };
            // WindowsPhone is the pre-defined platform ID for WP8.
            // You can get the application ID from the WMAppManifest.xml file
            record.AddPlatformAppId("WindowsPhone", "{7279d914-7983-4934-a0c0-0b603f644665}");
            // Publish the record using the proximity device
            PublishRecord(record, true);
        }

         private void PublishRecord(NdefRecord record, bool writeToTag)
        {
            if (_device == null) return;
            // Make sure we're not already publishing another message
            StopPublishingMessage(false);
            // Wrap the NDEF record into an NDEF message
            var message = new NdefMessage { record };
            // Convert the NDEF message to a byte array
            var msgArray = message.ToByteArray();
            // Publish the NDEF message to a tag or to another device, depending on the writeToTag parameter
            // Save the publication ID so that we can cancel publication later
            _publishingMessageId = _device.PublishBinaryMessage(("NDEF:WriteTag"), msgArray.AsBuffer(), MessageWrittenHandler);
            // Update status text for UI
            SetStatusOutput(string.Format((writeToTag ? AppResources.StatusWriteToTag : AppResources.StatusWriteToDevice), msgArray.Length, _publishingMessageId));

        }


        private void StopPublishingMessage(bool writeToStatusOutput)
        {
            if (_publishingMessageId != 0 && _device != null)
                // Stop publishing the message
                _device.StopPublishingMessage(_publishingMessageId);
            _publishingMessageId = 0;

            // Update status text for UI - only if activated
            if (writeToStatusOutput) SetStatusOutput(AppResources.StatusPublicationStopped);
        }
    }


        #endregion




}

