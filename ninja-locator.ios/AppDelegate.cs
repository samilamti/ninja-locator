using MonoTouch.Foundation;
using MonoTouch.UIKit;
using NinjaLocator.Core;
using NinjaLocator.Core.iOS;
using NinjaLocator.iOS.ViewControllers;
using NinjaLocator.iOS.Views;
using ServiceClient = NinjaLocator.Core.iOS.ServiceClient;

namespace NinjaLocator.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow _window;
        public static ServiceClient NinjaClient;
        public static UIDefinitions UiDefinitions = new UIDefinitions();
        readonly LocationService _locationService = new LocationService();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            var welcomeView = new WelcomeView(UIScreen.MainScreen.ApplicationFrame);
            _window.AddSubview(welcomeView);

            welcomeView.Done += () => {
                var rootController = new UINavigationController();
                _window.RootViewController = rootController;

                var gettingStarted = new GettingStartedViewController();
                gettingStarted.Done += () => _locationService.GetCurrentLocation(coordinate => {
                    var me = new Ninja {
                        GroupName = gettingStarted.GroupName,
                        Latitude = coordinate.Latitude,
                        Longitude = coordinate.Longitude,
                        NickName = gettingStarted.Nickname
                    };

                    NinjaClient = new ServiceClient(gettingStarted) {
                        AuthenticationProvider = gettingStarted.AuthenticationProvider
                    };

                    NinjaClient.LocateNinjas(me, ninjasLocated => InvokeOnMainThread(() => {
                        rootController.DismissViewController(true, null);
                        var mapView = new MapViewController(coordinate, ninjasLocated);
                        mapView.Title = me.GroupName;
                        rootController.PushViewController(mapView, true);
                    }));
                });

                rootController.PresentViewController(gettingStarted, true, null);
            };

            _window.MakeKeyAndVisible();

            return true;
        }
    }
}