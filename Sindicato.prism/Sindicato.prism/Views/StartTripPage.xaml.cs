using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Sindicato.common.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.Views
{
    public partial class StartTripPage : ContentPage
    {
        private readonly IGeolocationService _geolocationService;
        private readonly ISignalService _signalService;
        private static StartTripPage _instancia;

        public StartTripPage(IGeolocationService geolocationService,ISignalService signalService)
        {
            InitializeComponent();
            _geolocationService = geolocationService;
            _signalService = signalService;
            _instancia = this;
        }
        public static StartTripPage GetInstancia()
        {
            return _instancia;
        }
        public void AddPin(Position position,string address, string label, PinType pinType)
        {
            MyMap.Pins.Add(new Pin { 
                Address=address,
                Label=label,
                Position=position,
                Type=pinType
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveMapToCurrentPositionAsync();
        }

        private async void MoveMapToCurrentPositionAsync()
        {
            bool isLocationPermision = await CheckLocationPermisionsAsync();
            if (isLocationPermision)
            {
                MyMap.IsShowingUser = true;

                await _geolocationService.GeolocationAsync();
                if (_geolocationService.Latitude!=0 && _geolocationService.Longitude!=0)
                {
                    Position position = new Position(
                        _geolocationService.Latitude,
                        _geolocationService.Longitude);
                    MoveMap(position);
                }
            }
        }
        public void MoveMap(Position position)
        {
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            position,
            Distance.FromKilometers(.2)));
        }
        public void DrawLine(Position a, Position b)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Polygon polygon = new Polygon
                {
                    StrokeWidth = 10,
                    StrokeColor = Color.FromHex("#8D07F6"),
                    FillColor = Color.FromHex("#8D07F6"),
                    Geopath = { a, b }
                };

                MyMap.MapElements.Add(polygon);
            }
            else
            {
                AddPin(b, string.Empty, string.Empty, PinType.SavedPin);
            }

            MoveMap(b);
        }

        private async Task<bool> CheckLocationPermisionsAsync()
        {
            PermissionStatus permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            PermissionStatus permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            PermissionStatus permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            bool isLocationEnabled = permissionLocation == PermissionStatus.Granted ||
                                     permissionLocationAlways == PermissionStatus.Granted ||
                                     permissionLocationWhenInUse == PermissionStatus.Granted;
            if (isLocationEnabled)
            {
                return true;
            }

            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);

            permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            return permissionLocation == PermissionStatus.Granted ||
                   permissionLocationAlways == PermissionStatus.Granted ||
                   permissionLocationWhenInUse == PermissionStatus.Granted;
        }
    }
}
