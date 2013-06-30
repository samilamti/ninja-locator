using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace NinjaLocator.WindowsPhone
{
    public partial class Welcome : PhoneApplicationPage
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void OnLetMeInClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GettingStarted.xaml", UriKind.Relative));
        }
    }
}