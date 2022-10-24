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
using WSSindicato.Models.Response;

namespace Sindicato.prism.ViewModels
{
    public class RutasPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<RutasItemViewModel> _Rutas;
        private bool _isRunnig;

        public RutasPageViewModel(INavigationService navigationService,
            IApiService apiService):base(navigationService)
        {
            Title = "Rutas de viajes";
            _navigationService = navigationService;
            _apiService = apiService;
            GetRutasAsync();
        }

        public bool IsRunning
        {
            get => _isRunnig;
            set => SetProperty(ref _isRunnig, value);
        }
        public List<RutasItemViewModel> Rutas
        {
            get => _Rutas;
            set => SetProperty(ref _Rutas, value);
        }
        private async void GetRutasAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            Respuesta response = await _apiService.GetListAsync<RutasRequest>(url, "api","/Rutas");
            IsRunning = false;
            if (response.Data==null)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Mensaje,
                    "Aceptar");
                return;
            }
            var rutas = (List<RutasRequest>)response.Data;
            //Rutas = new ObservableCollection<RutasItemViewModel>(rutas);
            Rutas = rutas.Select(r => new RutasItemViewModel(_navigationService)
            {
                IdComunidad = r.IdComunidad,
                IdGrupo = r.IdGrupo,
                NombreComunidad = r.NombreComunidad,
                NombreGrupo = r.NombreGrupo,
                Estado = r.Estado,
                Rutas = r.Rutas
            }).ToList();
        }
    }
}
