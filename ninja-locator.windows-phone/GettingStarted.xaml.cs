using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.WindowsAzure.MobileServices;

namespace NinjaLocator.WindowsPhone
{
    public partial class GettingStarted : PhoneApplicationPage
    {
        public MobileServiceAuthenticationProvider AuthenticationProvider 
        { 
            get
            {
                var selectedItem = (ListPickerItem) authenticationProvider.SelectedItem;
                var selectedValue = ((string) selectedItem.Content).Replace(" ", String.Empty);
                var enumValue = (MobileServiceAuthenticationProvider)Enum.Parse(typeof (MobileServiceAuthenticationProvider), selectedValue);
                return enumValue;
            } 
        }
        public string Nickname { get { return nickName.Text; } }
        public string Group { get { return group.Text; } }

        public GettingStarted()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnFindNinjasClicked(object sender, RoutedEventArgs e)
        {
            var target = string.Format("/MainPage.xaml?provider={0}&nick={1}&group={2}", (int)AuthenticationProvider, Nickname, Group);
            NavigationService.Navigate(new Uri(target, UriKind.Relative));
        }
    }
}