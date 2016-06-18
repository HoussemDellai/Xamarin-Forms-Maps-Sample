using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapsApp
{
    public partial class SampleMapPage : ContentPage
    {
        public SampleMapPage()
        {
            InitializeComponent();


            var map = new Map(MapSpan.FromCenterAndRadius(
                new Position(36.8961, 10.1865),
                Distance.FromMiles(0.5)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var position1 = new Position(36.8961, 10.1865);
            var position2 = new Position(36.891, 10.181);
            var position3 = new Position(36.892, 10.182);
            var position4 = new Position(36.893, 10.183);
            var position5 = new Position(36.891, 10.185);
            var position6 = new Position(36.892, 10.187);

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "IntilaQ",
                Address = "www.intilaq.tn",
            };

            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "Telnet R&D",
                Address = "www.groupe-telnet.com"
            };

            var pin3 = new Pin
            {
                Type = PinType.Place,
                Position = position3,
                Label = "Telnet R&D",
                Address = "www.kromberg-schubert.com"
            };

            var pin4 = new Pin
            {
                Type = PinType.Place,
                Position = position4,
                Label = "Telnet R&D",
                Address = "www.kromberg-schubert.com"
            };

            var pin5 = new Pin
            {
                Type = PinType.Place,
                Position = position5,
                Label = "Telnet R&D",
                Address = "www.kromberg-schubert.com"
            };

            var pin6 = new Pin
            {
                Type = PinType.Place,
                Position = position6,
                Label = "Telnet R&D",
                Address = "www.kromberg-schubert.com"
            };

            map.Pins.Add(pin1);
            map.Pins.Add(pin2);
            map.Pins.Add(pin3);
            map.Pins.Add(pin4);
            map.Pins.Add(pin5);
            map.Pins.Add(pin6);

            Content = map;
        }
    }
}
