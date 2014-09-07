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
        public virtual DateTime BeginTime { get; set; }

        public Page1()
        {
            InitializeComponent();



        }

        private void ApplicationBarSaveButton_Click(object sender, EventArgs e)
        {
              String name = System.Guid.NewGuid().ToString();
              DateTime date = (DateTime)beginDatePicker.Value;
              DateTime time = (DateTime)beginTimePicker.Value;
              DateTime beginTime = date + time.TimeOfDay;

              // Make sure that the begin time has not already passed.
              if (beginTime < DateTime.Now)
              {
                  MessageBox.Show("the begin date must be in the future.");
                  return;
              }

              // Get the expiration time for the notification.
              date = (DateTime)expirationDatePicker.Value;
              time = (DateTime)expirationTimePicker.Value;
              DateTime expirationTime = date + time.TimeOfDay;

              // Make sure that the expiration time is after the begin time.
              if (expirationTime < beginTime)
              {
                  MessageBox.Show("expiration time must be after the begin time.");
                  return;
              }

              // Determine which recurrence radio button is checked.
              RecurrenceInterval recurrence = RecurrenceInterval.None;
              if (dailyRadioButton.IsChecked == true)
              {
                  recurrence = RecurrenceInterval.Daily;
              }
              else if (weeklyRadioButton.IsChecked == true)
              {
                  recurrence = RecurrenceInterval.Weekly;
              }
              else if (monthlyRadioButton.IsChecked == true)
              {
                  recurrence = RecurrenceInterval.Monthly;
              }
              else if (endOfMonthRadioButton.IsChecked == true)
              {
                  recurrence = RecurrenceInterval.EndOfMonth;
              }
              else if (yearlyRadioButton.IsChecked == true)
              {
                  recurrence = RecurrenceInterval.Yearly;
              }

              ///////////////////////////////////////////////
              Alarm alarm = new Alarm(name);
              alarm.Content = contentTextBox.Text;
              alarm.Sound = new Uri("/Ringtones/Ring01.wma", UriKind.Relative);
              alarm.BeginTime = beginTime;
              alarm.ExpirationTime = expirationTime;
              alarm.RecurrenceType = recurrence;

              ScheduledActionService.Add(alarm);
              // Navigate back to the main reminder list page.
              if (this.NavigationService.CanGoBack)
              {
                  NavigationService.GoBack();
              }


        }

        private void ApplicationBarSaveButton_Click_1(object sender, EventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}