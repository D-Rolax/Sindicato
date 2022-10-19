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
using System.Timers;
using System.Threading.Tasks;
using WSSindicato.Models.Response;
using Xamarin.Forms.Maps;
using Sindicato.prism.Helpers;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Sindicato.common.Helpers;

namespace Sindicato.prism.ViewModels
{
    public class StartTripPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGeolocationService _geolocationService;
        private readonly IApiService _apiService;
        private string _buttonLabel;
        private bool _isSecondButtonVisible;
        private bool _isRunnig;
        private bool _isEnable;
        private Position _position;
        private Timer _timer;
        private Grupos _grupo;
        private Comunidades _comunidad;
        private ObservableCollection<Grupos> _Grupos;
        private ObservableCollection<Comunidades> _Comunidades;
        private DelegateCommand _getAddressComand;
        private DelegateCommand _startTripCommand;
        private RutasDetailsRequest _rutasDetailsRequest;
        private string _url;
        private TokenResponse _token;
        private DelegateCommand _CancelTripComan;

        public StartTripPageViewModel(INavigationService navigationService,
            IGeolocationService geolocationService,
            IApiService apiService):base(navigationService)
        {
            Title = "Iniciar Viaje";
            _navigationService = navigationService;
            _geolocationService = geolocationService;
            _apiService = apiService;
            _rutasDetailsRequest = new RutasDetailsRequest { Rutas=new List<RutasDetailRequest>() };
            GetGrupoAsync();
            GetComunidadAsync();
            LoadSourceAsync();
            IsEnabled = true;
            ButtonLabel = "Iniciar Viaje";
        }
        public DelegateCommand CancelTripCommand => _getAddressComand ?? (_getAddressComand = new DelegateCommand(CancelTripAsync));

        public DelegateCommand GetAddressCommand => _getAddressComand ?? (_getAddressComand = new DelegateCommand(LoadSourceAsync));
        public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

        public string Source {
            get => _buttonLabel; 
            set => SetProperty(ref _buttonLabel, value); 
        }
        public ObservableCollection<Grupos> Grupos
        {
            get => _Grupos;
            set => SetProperty(ref _Grupos, value);
        }
        public Grupos Grupo
        {
            get => _grupo;
            set => SetProperty(ref _grupo, value);
        }
        public Comunidades Comunidad
        {
            get => _comunidad;
            set => SetProperty(ref _comunidad, value);
        }
        public ObservableCollection<Comunidades> Comunidades
        {
            get => _Comunidades;
            set => SetProperty(ref _Comunidades, value);
        }
        public string ButtonLabel
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }
        public bool IsEnabled
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }
        public bool IsRunning
        {
            get => _isRunnig;
            set => SetProperty(ref _isRunnig, value);
        }
        public bool IsSecondButtonVisible
        {
            get => _isSecondButtonVisible;
            set => SetProperty(ref _isSecondButtonVisible, value);
        }
        private async void StartTripAsync()
        {
            bool isValid = await ValidationDataAsync();
            if (!isValid)
            {
                return;
            }
            if (IsSecondButtonVisible)
            {
                await EndTripAsync();
            }
            else
            {
                await BeginTripAsync();
            }
        }
        private async void CancelTripAsync()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Confirmar","Esta seguro de cancelar el viaje","Si","No");
            if (!answer)
            {
                return;
            }
            IsRunning = true;

            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            _timer.Stop();
            RutasResponse model = new RutasResponse
            {
                IdComunidad = Comunidad.Id,
                IdGrupo=Grupo.Id
            };
            await _apiService.DeleteRutasAsync(_url, "api", "/rutas/delete",model);
            IsRunning = false;
            await _navigationService.GoBackToRootAsync();
        }
        private async Task EndTripAsync()
        {
            _timer.Stop();
            if (_rutasDetailsRequest.Rutas.Count > 0)
            {
                await SendTripDetailsAsync();
            }
            IsRunning = true;
            await _navigationService.GoBackToRootAsync();
        }

        private async  Task BeginTripAsync()
        {
            IsRunning = true;
            _url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            IsSecondButtonVisible = true;
            ButtonLabel = "Fin de viaje";
            StartTripPage.GetInstancia().AddPin(_position, Source,"Inicio",PinType.Place);
            _token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            IsRunning = false;
            //IsEnabled = false;
            _timer = new Timer
            {
                Interval = 1000
            };

            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await _geolocationService.GeolocationAsync();
            if (_geolocationService.Latitude == 0 && _geolocationService.Longitude == 0)
            {
                return;
            }

            Position previousPosition = new Position(_position.Latitude, _position.Longitude);
            _position = new Position(_geolocationService.Latitude, _geolocationService.Longitude);
            double distance = GeoHelper.GetDistance(previousPosition, _position, UnitOfLength.Kilometers);

            if (distance < 0.003 || double.IsNaN(distance))
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                StartTripPage.GetInstancia().DrawLine(previousPosition, _position);
            });
            _rutasDetailsRequest.Rutas.Add(new RutasDetailRequest
            {
                Latitud = _position.Latitude,
                Longitud = _position.Longitude
            });
            if (_rutasDetailsRequest.Rutas.Count > 9)
            {
                SendTripDetailsAsync();
            }
        }

        private async Task SendTripDetailsAsync()
        {
            RutasDetailsRequest rutasDetailsRequestCloned = CloneTripDetailsRequest(_rutasDetailsRequest);
            _rutasDetailsRequest.Rutas.Clear();
            await _apiService.AddRutas(_url, "api", "/Rutas", rutasDetailsRequestCloned, "bearer", _token.Token);
        }

        private RutasDetailsRequest CloneTripDetailsRequest(RutasDetailsRequest rutasDetailRequest)
        {
            RutasDetailsRequest rutasDetailsRequestClone = new RutasDetailsRequest
            {
                IdComunidad=Comunidad.Id,
                IdGrupo=Grupo.Id,
                Rutas= rutasDetailRequest.Rutas.Select(d => new RutasDetailRequest
                {
                    Latitud = d.Latitud,
                    Longitud = d.Longitud
                }).ToList()
            };

            return rutasDetailsRequestClone;
        }

        //Validamos los campos
        private async Task<bool> ValidationDataAsync()
        {
            if (Grupo == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar un grupo", "Aceptar");
                return false;
            }
            if (Comunidad == null)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe seleccionar una Comunidad", "Aceptar");
                return false;
            }
            return true;
        }
        public async void GetGrupoAsync()
        {
            IsRunning = true;
            _url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            Respuesta respuesta = await _apiService.GetGrupos(_url,"api","/grupo");
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
            _url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            Respuesta respuesta = await _apiService.GetComunidad(_url, "api", "/comunidad");
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
            //Comunidad = Comunidades.FirstOrDefault(c => c.Nombre == Comunidad.Nombre);

        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (IsSecondButtonVisible && _timer != null)
            {
                _timer.Start();
            }
        }

        //Obtienen la direccion
        public async void LoadSourceAsync()
        {
            IsEnabled = false;
            await _geolocationService.GeolocationAsync();
            if (_geolocationService.Latitude==0 && _geolocationService.Longitude==0)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo encontrar la Direccion debe encender su GPS", "Aceptar");
                await _navigationService.GoBackAsync();
                return;

            }
            _position = new Position(_geolocationService.Latitude, _geolocationService.Longitude);
            Geocoder geoCoder = new Geocoder();
            IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(_position);
            List<string> addresses = new List<string>(sources);
            if (addresses.Count > 0)
            {
                Source = addresses[0];
            }
            IsEnabled = true;
        }

    }
}
