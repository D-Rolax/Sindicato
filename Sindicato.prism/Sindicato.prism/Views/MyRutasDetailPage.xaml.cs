using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.Views
{
    public partial class MyRutasDetailPage : ContentPage
    {
        private static MyRutasDetailPage _instancia;
        private double _distance;
        private Position _position;
        public MyRutasDetailPage()
        {
            InitializeComponent();
            _instancia = this;
            _distance = 1;
        }
        public static MyRutasDetailPage GetInstancia()
        {
            return _instancia;
        }
        public async void DrawMap(RutasRequest request)
        {
            int cont = request.Rutas.Count;
            _position=new Position(request.Rutas[0].Latitud,request.Rutas[0].Longitud);
            Geocoder geoCoder = new Geocoder();
            IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(_position);
            List<string> addresses = new List<string>(sources);
            if (addresses.Count > 0)
            {
                string Source = addresses[0];
                AddPin(_position, Source, "Inicio de viaje", PinType.Place);
                MoveMap(_position);
            }
            if (addresses.Count > 1)
            {
                _position = new Position(request.Rutas[cont - 1].Latitud, request.Rutas[cont - 1].Longitud);
                sources = await geoCoder.GetAddressesForPositionAsync(_position);
                addresses = new List<string>(sources);
                string Source = addresses[0];
                AddPin(_position, Source, "Fin de viaje", PinType.Place);
                _position = new Position(request.Rutas[cont / 2].Latitud, request.Rutas[cont / 2].Longitud);
                MoveMap(_position);
            }
            for (int i = 0; i < request.Rutas.Count-1; i++)
            {
                Position a = new Position(request.Rutas[i].Latitud, request.Rutas[i].Longitud);
                Position b = new Position(request.Rutas[i+1].Latitud, request.Rutas[i+1].Longitud);
                DrawLine(a,b);
            }
        }

        private void DrawLine(Position a, Position b)
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
        }
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
        private void MoveMap(Position position)
        {
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(_distance)));
        }
        private void MySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            _distance = e.NewValue;
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromKilometers(_distance)));
        }
    }
}
