using System.Collections.Generic;
using System.Drawing;
using MonoTouch.CoreLocation;
using MonoTouch.MapKit;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using NinjaLocator.Core;

namespace NinjaLocator.iOS.ViewControllers
{
    [Register("MapViewController")]
    public class MapViewController : UIViewController
    {
        private readonly CLLocationCoordinate2D _centerCoordinate;
        private readonly IEnumerable<Ninja> _ninjas;
        private MKMapView _mapView;
        const int LabelPinSubView = 1;

        public MapViewController(CLLocationCoordinate2D centerCoordinate, IEnumerable<Ninja> ninjas)
        {
            _centerCoordinate = centerCoordinate;
            _ninjas = ninjas;
        }

        public override void ViewDidLoad()
        {
            CreateMapView();
            base.ViewDidLoad();

            foreach (var ninja in _ninjas)
            {
                _mapView.AddAnnotation(new NinjaAnnotation(ninja));
            }
        }

        static MKCoordinateRegion BuildVisibleRegion(CLLocationCoordinate2D currentLocation)
        {
            var span = new MKCoordinateSpan(5, 5);
            var region = new MKCoordinateRegion(currentLocation, span);
            return region;
        }

        void CreateMapView()
        {
            if (_mapView != null)
                return;

            _mapView = new MKMapView {ShowsUserLocation = true, GetViewForAnnotation = GetViewForAnnotation};

            View = _mapView;

            var visibleRegion = BuildVisibleRegion(_centerCoordinate);
            _mapView.SetRegion(visibleRegion, animated: true);
        }

        MKAnnotationView GetViewForAnnotation(MKMapView mapView, NSObject annotation)
        {
            var checkpoint = annotation as NinjaAnnotation;
            if (checkpoint == null)
                return null;

            MKAnnotationView view = CreatePushPin(checkpoint);
            view.CanShowCallout = false;
            ((UILabel)view.ViewWithTag(LabelPinSubView)).Text = checkpoint.Title;

            return view;
        }

        MKAnnotationView CreatePushPin(NinjaAnnotation annotation)
        {
            const string reuseIdentifier = "NinjaPushPin";
            var view = _mapView.DequeueReusableAnnotation(reuseIdentifier) as MKPinAnnotationView;

            if (view != null)
                return view;

            view = new MKPinAnnotationView(annotation, reuseIdentifier)
                {
                    PinColor = MKPinAnnotationColor.Purple,
                    AnimatesDrop = true
                };
            view.AddSubview(CreateLabel(annotation));
            return view;
        }

        UILabel CreateLabel(NinjaAnnotation annotation)
        {
            var label = new UILabel(new RectangleF(-10, 27, 80, 40))
            {
                BackgroundColor = UIColor.Clear,
                TextColor = UIColor.Black,
                ShadowColor = UIColor.White,
                AdjustsFontSizeToFitWidth = true,
                Tag = LabelPinSubView,
                Font = UIFont.BoldSystemFontOfSize(14),
                Text = annotation.Title
            };
            return label;
        }
    }
}