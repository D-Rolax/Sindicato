using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using WSSindicato.Models.Response;

namespace Sindicato.prism.ViewModels
{
    public class MyRutasDetailPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private RutasRequest _rutas;
        private string _grupo;
        private string _comunidad;
        private string _estado;
        public MyRutasDetailPageViewModel(INavigationService navigationService,
            IApiService apiService):base(navigationService)
        {
            Title = "Rutas de Viaje";
            _apiService = apiService;
        }
        public RutasRequest Rutas
        {
            get => _rutas;
            set => SetProperty(ref _rutas, value);
        }
        public string Grupo
        {
            get => _grupo;
            set => SetProperty(ref _grupo, value);
        }
        public string Comunidad
        {
            get => _comunidad;
            set => SetProperty(ref _comunidad, value);
        }
        public string Estado
        {
            get => _estado;
            set => SetProperty(ref _estado, value);
        }
        public string Distancia
        {
            get => _estado;
            set => SetProperty(ref _estado, value);
        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                await App.Current.MainPage.DisplayAlert("Error de Conccion", "No hay conexión a Internet", "Aceptar");
                return;
            }
            base.OnNavigatedTo(parameters);
            Rutas = parameters.GetValue<RutasRequest>("rutas");
            Grupo = Rutas.NombreGrupo;
            Comunidad = Rutas.NombreComunidad;
            Estado = Rutas.Estado;
            Title = Rutas.NombreComunidad;
            MyRutasDetailPage.GetInstancia().DrawMap(Rutas);
        }
    }
}
