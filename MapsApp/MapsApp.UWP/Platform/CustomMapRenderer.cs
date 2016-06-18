using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using MapsApp;
using MapsApp.UWP.Platform;
using MapsApp.UWP.UI;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapsApp.UWP.Platform
{
    public class CustomMapRenderer : MapRenderer
    {
        private MapControl _nativeMap;
        private List<CustomPin> _customPins;
        private XamarinMapOverlay _mapOverlay;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _nativeMap.MapElementClick -= OnMapElementClick;
                _nativeMap.Children.Clear();
                _mapOverlay = null;
                _nativeMap = null;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                _nativeMap = Control as MapControl;
                _customPins = formsMap.CustomPins;

                _nativeMap.Children.Clear();
                _nativeMap.MapElementClick += OnMapElementClick;

                foreach (var pin in _customPins)
                {
                    var snPosition = new BasicGeoposition { Latitude = pin.Pin.Position.Latitude, Longitude = pin.Pin.Position.Longitude };
                    var snPoint = new Geopoint(snPosition);

                    var mapIcon = new MapIcon
                    {
                        Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///pin.png")),
                        CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,
                        Location = snPoint,
                        NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0)
                    };

                    _nativeMap.MapElements.Add(mapIcon);
                }
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {

            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;

            if (mapIcon != null)
            {
                var customPin = GetCustomPin(mapIcon.Location.Position);

                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                _mapOverlay = new XamarinMapOverlay(customPin);

                var xamarinMapOverlays = _nativeMap.Children.Where(o => o is XamarinMapOverlay);

                // Remove shown XamarinMapOverlay
                foreach (var xamarinMapOverlay in xamarinMapOverlays)
                {
                    _nativeMap.Children.Remove(xamarinMapOverlay);
                }

                var snPosition = new BasicGeoposition { Latitude = customPin.Pin.Position.Latitude, Longitude = customPin.Pin.Position.Longitude };
                var snPoint = new Geopoint(snPosition);

                _nativeMap.Children.Add(_mapOverlay);
                MapControl.SetLocation(_mapOverlay, snPoint);
                MapControl.SetNormalizedAnchorPoint(_mapOverlay, new Windows.Foundation.Point(0.5, 1.0));
            }
        }

        private CustomPin GetCustomPin(BasicGeoposition position)
        {
            var pos = new Position(position.Latitude, position.Longitude);
            foreach (var pin in _customPins)
            {
                if (pin.Pin.Position == pos)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}
