using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models.Response;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.ViewModels
{
    public class StartTripPageViewModel : ViewModelBase
    {
        private readonly IGeolocationService _geolocationService;
        private readonly IApiService _apiService;
        private string _buttonLabel;
        private bool _isRunnig;
        private ObservableCollection<Grupos> _Grupos;
        private ObservableCollection<Comunidades> _Comunidades;
        private DelegateCommand _getAddressComand;
        private DelegateCommand _startTripCommand;

        public StartTripPageViewModel(INavigationService navigationService,
            IGeolocationService geolocationService,
            IApiService apiService):base(navigationService)
        {
            Title = "Iniciar Viaje";
            _geolocationService = geolocationService;
            _apiService = apiService;
            GetGrupoAsync();
            GetComunidadAsync();
        }
        public DelegateCommand GetAddressComand => _getAddressComand ?? (_getAddressComand = new DelegateCommand(LoadSourceAsync));
        public DelegateCommand StartTripComand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

        public string Source {
            get => _buttonLabel; 
            set => SetProperty(ref _buttonLabel, value); 
        }
        public ObservableCollection<Grupos> Grupos
        {
            get => _Grupos;
            set => SetProperty(ref _Grupos, value);
        }
        public ObservableCollection<Comunidades> Comunidades
        {
            get => _Comunidades;
            set => SetProperty(ref _Comunidades, value);
        }
        public bool IsRunning
        {
            get => _isRunnig;
            set => SetProperty(ref _isRunnig, value);
        }
        private async  void StartTripAsync()
        {
            //bool isValid = await ValidationDataAsync();
            //if (!isValid)
            //{
            //    return;
            //}
        }
        //Validamos los campos
        //private async Task<bool> ValidationDataAsync()
        //{
        //    if (string.IsNullOrEmpty(Grupo))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar un grupo", "Aceptar");
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Comunidad))
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar una Comunidad", "Aceptar");
        //        return false;
        //    }
        //    return true;
        //}
        public async void GetGrupoAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Respuesta respuesta = await _apiService.GetGrupos(url,"api","/grupo");
            IsRunning = false;
            if (respuesta.Data==null)
            {
                await App.Current.MainPage.DisplayAlert(
                "Error",
                respuesta.Mensaje,
                "Aceptar");
                return;
            }
            List<Grupos> grupos = (List<Grupos>)respuesta.Data;
            Grupos=new ObservableCollection<Grupos>(grupos);
        }
        public async void GetComunidadAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Respuesta respuesta = await _apiService.GetComunidad(url, "api", "/comunidad");
            IsRunning = false;
            if (respuesta.Data == null)
            {
                await App.Current.MainPage.DisplayAlert(
                "Error",
                respuesta.Mensaje,
                "Aceptar");
                return;
            }
            List<Comunidades> comunidades = (List<Comunidades>)respuesta.Data;
            Comunidades = new ObservableCollection<Comunidades>(comunidades);
        }
        //Obtienen la direccion
        public async void LoadSourceAsync()
        {
            await _geolocationService.GeolocationAsync();
            if (_geolocationService.Latitude!=0 && _geolocationService.Longitude!=0)
            {
                Position position = new Position(_geolocationService.Latitude, _geolocationService.Longitude);
                Geocoder geoCoder = new Geocoder();
                IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(position);
                List<string> addresses = new List<string>(sources);
                if (addresses.Count>0)
                {
                    Source = addresses[0];
                }
            }
        }
    }
}
