using System.Device.Location;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using Microsoft.WindowsAzure.MobileServices;
using NinjaLocator.Core;
using ServiceClient = NinjaLocator.Core.WindowsPhone.ServiceClient;

namespace NinjaLocator.WindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private NinjaLocator.Core.WindowsPhone.ServiceClient serviceClient;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (serviceClient != null) return;

            var loginProvider = (MobileServiceAuthenticationProvider) int.Parse(NavigationContext.QueryString["provider"]);
            var nickname = NavigationContext.QueryString["nick"];
            var group = NavigationContext.QueryString["group"];
            this.group.Text = group;

            serviceClient = new ServiceClient {AuthenticationProvider = loginProvider};
            var me = new Ninja
            {
                GroupName = group,
                NickName = nickname
            };

            var watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += (sender, args) => {
                var lat = /*args.Position.Location.Latitude*/ 55.7091;
                var lon = /*args.Position.Location.Longitude*/ 013.1982;
                var geoCoordinate = new GeoCoordinate(lat, lon);
                map.SetView(geoCoordinate, 6, MapAnimationKind.Linear);
                me.Latitude = lat;
                me.Longitude = lon;
                serviceClient.LocateNinjas(me, ninjas => Dispatcher.BeginInvoke(delegate
                {
                    var children = MapExtensions.GetChildren(map);
                    foreach (var ninja in ninjas)
                    {
                        var ninjaLocation = new GeoCoordinate(ninja.Latitude, ninja.Longitude);
                        var pushpin = new Pushpin { GeoCoordinate = ninjaLocation, Content = ninja.NickName };
                        children.Add(pushpin);
                    }
                    children.Add(new Pushpin {GeoCoordinate = geoCoordinate, Content = me.NickName, Background = new SolidColorBrush(Colors.Orange) });
                }));
                watcher.Stop();
            };
            watcher.Start();
        }
    }
}