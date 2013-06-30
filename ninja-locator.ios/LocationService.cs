using System;
using MonoTouch.CoreLocation;
using MonoTouch.UIKit;

namespace NinjaLocator.iOS
{
    public class LocationService
    {
        private readonly CLLocationManager locationManager;

        public LocationService()
        {
            locationManager = new CLLocationManager();
        }

        public void GetCurrentLocation(Action<CLLocationCoordinate2D> success)
        {
            locationManager.StartUpdatingLocation();
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                locationManager.LocationsUpdated += (sender, e) =>
                    {
                        locationManager.StopUpdatingLocation();
                        success(e.Locations[e.Locations.Length - 1].Coordinate);
                    };
            }
            else
            {
                // this won't be called on iOS 6 (deprecated)
                locationManager.UpdatedLocation += (sender, e) =>
                    {
                        locationManager.StopUpdatingLocation();
                        success(e.NewLocation.Coordinate);
                    };
            }
        }
    }
}

