using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using Sindicato.prism.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.Views
{
    public partial class VerViajePage : ContentPage
    {
        private static VerViajePage _instance;
        private readonly ILocatorCliente _locatorCliente;
        private readonly IGeolocationService _geolocationService;
        private Position _position;

        public VerViajePage(ILocatorCliente locatorCliente, IGeolocationService geolocationService)
        {
            InitializeComponent();
            _instance = this;
            _locatorCliente = locatorCliente;
            _geolocationService = geolocationService;
        }
        public static VerViajePage getInstancia()
        {
            return _instance;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveMapToCurrentPositionAsync();
        }

        public async void MoveMapToCurrentPositionAsync()
        {
            MyMap.IsShowingUser = true;

            await _geolocationService.GeolocationAsync();
            if (_locatorCliente.Latitude != 0 && _locatorCliente.Longitude != 0)
            {
                Position position = new Position(
                    _locatorCliente.Latitude,
                    _locatorCliente.Longitude);
                MoveMap(position);
            }
        }

        public void AddPin(Position position, string address, string label, PinType pinType)
        {
            
            if (_position==position)
            {
                MyMap.Pins.Add(new Pin
                {
                    Address = address,
                    Label = label,
                    Position = position,
                    Type = pinType,
                });
            }
            else
            {
                MyMap.Pins.Clear();
            }
            _position = new Position(position.Latitude,position.Longitude);
        }
        private void MoveMap(Position position)
        {
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));
        }
    }
}
