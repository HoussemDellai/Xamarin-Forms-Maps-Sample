using System;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MapsApp.UWP.UI
{
    public sealed partial class XamarinMapOverlay : UserControl
    {
        readonly CustomPin _customPin;

        public XamarinMapOverlay(CustomPin pin)
        {
            InitializeComponent();
            _customPin = pin;
            SetupData();
        }

        void SetupData()
        {
            Label.Text = _customPin.Pin.Label;
            Address.Text = _customPin.Pin.Address;
        }

        private async void OnInfoButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(_customPin.Url));
        }

    }
}
