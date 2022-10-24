using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WSSindicato.Models.Response;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.Helpers
{
    public class LocatorCliente:ILocatorCliente
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        private readonly IApiService _apiService;
        private string _url;
        private Timer _timer;
        private Position _position;
        public LocatorCliente(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task GeoclienteAsync()
        {
            _url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            _timer = new Timer
            {
                Interval = 1000
            };

            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Task.Run(async () =>
            {
                RutasRequest rutas = new RutasRequest
                {
                    IdComunidad = 3,
                    IdGrupo = 5
                };
                Respuesta respuesta = await _apiService.GetRutas(_url, "api", "/traking", rutas);
                if (respuesta.Data == null)
                {
                    await App.Current.MainPage.DisplayAlert(
                    "Error",
                    respuesta.Mensaje,
                    "Aceptar");
                    return;
                }
                UnasolaRuta cordenadas = (UnasolaRuta)respuesta.Data;
                Latitude = cordenadas.Latitud;
                Longitude = cordenadas.Longitud;
            _position = new Position(Latitude,Longitude);
                //VerViajePage.getInstancia().AddPin(_position, string.Empty, "Vehiculo", PinType.Place);
            });
        }
    }
}
