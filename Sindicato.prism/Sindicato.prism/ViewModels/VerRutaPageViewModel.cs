using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Helpers;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using WSSindicato.Hubs;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.ViewModels
{
    public class VerRutaPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ISignalService _signalService;
        private DelegateCommand _comentariCommand;
        private List<DatosUsuarioRequest> _user;
        private string _LbConnect;
        private double _latitud;
        private double _longitud;
        private Position _position;
        public VerRutaPageViewModel(INavigationService navigationService,
            ISignalService signalService) :base(navigationService)
        {
            Title = "Viajes";
            _navigationService=navigationService;
            _signalService = signalService;
            _signalService.MessageReceived += signalService_MessageReceived;
            _signalService.Connecting += signalService_Connecting;
            _signalService.Connected += signalService_Connected;
            ConctarSingnalR();
        }
        public string LbConecta
        {
            get => _LbConnect;
            set => SetProperty(ref _LbConnect, value);
        }
        public double Latitud
        {
            get => _latitud;
            set => SetProperty(ref _latitud, value);
        }
        public double Longitud
        {
            get => _longitud;
            set => SetProperty(ref _longitud, value);
        }
        public DelegateCommand ComentarioCommand => _comentariCommand ?? (_comentariCommand = new DelegateCommand(ComentarioAsync));

        private async void ComentarioAsync()
        {
            await _navigationService.NavigateAsync(nameof(EndTrip));
        }
        private void ConctarSingnalR()
        {
            _user = JsonConvert.DeserializeObject<List<DatosUsuarioRequest>>(Settings.User);
            SignalRService.DeviceId = _user[0].Id;
            _signalService.StartWidhReconnectionAsync();
        }
        private void signalService_Connected(object sender, EventArgs e)
        {
            LbConecta = "Conectado";
        }

        private void signalService_Connecting(object sender, EventArgs e)
        {
            LbConecta = "Conectando...";
        }

        private void signalService_MessageReceived(object sender, MessageItem e)
        {
            Latitud = e.Latitud;
            Longitud = e.Longitud;
            _position = new Position(e.Latitud, e.Longitud);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                VerRutaPage.GetInstancia().AddPin(_position, string.Empty, "Vehiculo en movimineto", PinType.Place);
            });
          
        }
    }
}
