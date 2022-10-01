using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppMovil.ViewModels
{
    public class SindicatoMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SindicatoMasterDetailPageViewModel(INavigationService navigationService):base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MeniItemViewModel> Menus { get; set; }
        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icono= "ic_airport_shuttle",
                    PagNavigation="HomePage",
                    Titulo="Nuevo viaje"
                    
                },
                new Menu
                {
                    Icono="ic_featured_play_list",
                    PagNavigation="ReservasPage",
                    Titulo="Reservas de Pasajes"
                    
                },
                new Menu
                {
                    Icono="ic_access_alarm",
                    PagNavigation="HorarioPage",
                    Titulo="Horarios de Salida"
                },
                new Menu
                {
                    Icono="ic_login",
                    PagNavigation="LoginPage",
                    Titulo="Login"
                } 
            };
            Menus = new ObservableCollection<MeniItemViewModel>(
              menus.Select(m => new MeniItemViewModel(_navigationService)
              {
                  Icono= m.Icono,
                  PagNavigation = m.PagNavigation,
                  Titulo = m.Titulo
              }).ToList());

        }
    }
}
