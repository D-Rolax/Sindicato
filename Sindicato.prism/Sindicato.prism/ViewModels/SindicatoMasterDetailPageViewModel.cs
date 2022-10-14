using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Helpers;
using Sindicato.common.Models.Response;
using Sindicato.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WSSindicato.Models.Response;

namespace Sindicato.prism.ViewModels
{
    public class SindicatoMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<DatosUsuario> _user;

        public SindicatoMasterDetailPageViewModel(INavigationService navigationService):base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }
        public List<DatosUsuario> user
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                user = JsonConvert.DeserializeObject <List<DatosUsuario>>(Settings.User);
            }
        }
        
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu{
                    Icono="ic_airport_shuttle",
                    PagNavigation="HomePage",
                    Titulo="Nuevo viaje"
                },
                new Menu{
                    Icono="ic_featured_play_list",
                    PagNavigation="ResarvasPage",
                    Titulo="Reserva de pasajes"
                },
                new Menu{
                    Icono="ic_access_alarm",
                    PagNavigation="HorariosPage",
                    Titulo="Horarios de salida"
                },
                new Menu{
                    Icono="ic_add_road",
                    PagNavigation="RutasPage",
                    Titulo="Rutas de viaje"
                },
                new Menu{
                    Icono="ic_login",
                    PagNavigation="LoginPage",
                    Titulo=Settings.IsLogin?"Cerrar sesión":"Login"
                },
            };
            Menus = new ObservableCollection<MenuItemViewModel>(
            menus.Select(m => new MenuItemViewModel(_navigationService)
            {
                Icono = m.Icono,
                PagNavigation = m.PagNavigation,
                Titulo = m.Titulo
            }).ToList());
        }
    }
}
