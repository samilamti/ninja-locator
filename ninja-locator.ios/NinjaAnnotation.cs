using MonoTouch.CoreLocation;
using MonoTouch.MapKit;
using NinjaLocator.Core;

namespace NinjaLocator.iOS
{
    public class NinjaAnnotation : MKPointAnnotation
    {
        private readonly string _title;
        private CLLocationCoordinate2D _coordinate;

        public NinjaAnnotation(Ninja ninja)
        {
            _title = ninja.NickName;
            _coordinate = new CLLocationCoordinate2D(ninja.Latitude, ninja.Longitude);
        }

        public override CLLocationCoordinate2D Coordinate { get { return _coordinate; } set { _coordinate = value; } }
        public override string Title { get { return _title; } }
    }
}
