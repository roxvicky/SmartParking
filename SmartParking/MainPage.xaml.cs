using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SmartParking.Resources;
using System.IO;
using System.IO.IsolatedStorage;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Diagnostics;
using SQLite;

namespace SmartParking
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void ApplicationBarIconButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Developed by Kittin ");// TODO: Add event handler implementation here.
        }

        private void ApplicationBarMenuItem_Click_1(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/eTags.xaml", UriKind.Relative));// TODO: Add event handler implementation here.
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
