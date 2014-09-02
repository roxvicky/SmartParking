using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Scheduler;


namespace SmartParking
{
    public partial class Page1 : PhoneApplicationPage
    {


        public Page1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            AddAlaram();

        }

        void AddAlaram()
        {

            Alarm alarm = new Alarm(txtName.Text.ToString());

            alarm.Content = txtContent.ToString();

            DateTime date = (DateTime)datePicker1.Value;

            DateTime time = (DateTime)timePicker1.Value;

            DateTime beginTime = date + time.TimeOfDay;

            alarm.BeginTime = beginTime;

            DateTime date1 = (DateTime)datePicker1.Value;

            DateTime time1 = (DateTime)timePicker1.Value;

            DateTime exptime = date1 + time1.TimeOfDay;

            alarm.ExpirationTime = exptime;

            alarm.RecurrenceType = RecurrenceInterval.Daily;

            ScheduledActionService.Add(alarm);

        }





    }
}