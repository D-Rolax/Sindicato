using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using WSSindicato.Models.Response;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.ViewModels
{
    public class VerViajePageViewModel : ViewModelBase
    {
        private string _url;
        private readonly IApiService _apiService;
        private Timer _timer;
        private Position _position;
        public VerViajePageViewModel(INavigationService navigationService, IApiService apiService) :base(navigationService)
        {
            Title = "Ver los Viajes";
            _apiService = apiService;
            BeginTripAsync();
        }
        private async Task BeginTripAsync()
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
                    IdComunidad = 1,
                    IdGrupo = 1
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
                UnasolaRuta rutasReponse = (UnasolaRuta)respuesta.Data;

                _position = new Position(rutasReponse.Latitud, rutasReponse.Longitud);
            });
            MainThread.BeginInvokeOnMainThread(() =>
            {
                VerViajePage.getInstancia().AddPin(_position,string.Empty,"Vehiculo en movimineto",PinType.Place);
            });
        }
    }
}
