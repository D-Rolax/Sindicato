using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WSSindicato.Models.Response;

namespace Sindicato.prism.ViewModels
{
    public class RutasPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private ObservableCollection<RutasResponse> _Rutas;
        private bool _isRunnig;

        public RutasPageViewModel(INavigationService navigationService,
            IApiService apiService):base(navigationService)
        {
            Title = "Rutas de viajes";
            _apiService = apiService;
            GetRutasAsync();
        }
        public bool IsRunning
        {
            get => _isRunnig;
            set => SetProperty(ref _isRunnig, value);
        }
        public ObservableCollection<RutasResponse> Rutas
        {
            get => _Rutas;
            set => SetProperty(ref _Rutas, value);
        }
        private async void GetRutasAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Respuesta response = await _apiService.GetListRutasAsync<RutasResponse>(url, "api","/Rutas");
            IsRunning = false;
            if (response.Data==null)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Mensaje,
                    "Aceptar");
                return;
            }
            List<RutasResponse> rutas = (List<RutasResponse>)response.Data;
            Rutas = new ObservableCollection<RutasResponse>(rutas);
        }
    }
}
