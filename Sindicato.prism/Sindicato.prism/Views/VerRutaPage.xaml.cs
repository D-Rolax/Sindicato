using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Sindicato.common.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.Views
{
    public partial class VerRutaPage : ContentPage
    {
        private static VerRutaPage _instancia;
        private readonly ISignalService _signalService;
        private readonly IGeolocationService _geolocatorService;
        private Position _position1;
        private Position _position2;
        public VerRutaPage(IGeolocationService geolocationService, ISignalService signalService)
        {
            InitializeComponent();
            _signalService = signalService;
            _geolocatorService = geolocationService;
            _instancia = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveMapToCurrentPositionAsync();
        }
        public static VerRutaPage GetInstancia()
        {
            return _instancia;
        }
        private async void MoveMapToCurrentPositionAsync()
        {
            bool isLocationPermision = await CheckLocationPermisionsAsync();

            if (isLocationPermision)
            {
                MyMap.IsShowingUser = true;

                await _geolocatorService.GeolocationAsync();
                if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
                {
                    Position position = new Position(
                        _geolocatorService.Latitude,
                        _geolocatorService.Longitude);
                    MoveMap(position);
                }
            }
        }
        private void MoveMap(Position position)
        {
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            position,
            Distance.FromKilometers(1)));
        }
        int cont = 1;
        public void AddPin(Position position, string address, string label, PinType pinType)
        {
            MyMap.Pins.Add(new Pin
            {
                Address = address,
                Label = label,
                Position = position,
                Type = pinType
            });
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
