using Prism.Commands;
using Prism.Navigation;
using Sindicato.common.Helpers;
using Sindicato.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.prism.ViewModels
{
    public class MenuItemViewModel:Menu
    {

        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;
        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (PagNavigation=="LoginPage" && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.User = null;
                Settings.Token = null;
            }
            await _navigationService.NavigateAsync($"/SindicatoMasterDetailPage/NavigationPage/{PagNavigation}");

        }
    }
}
