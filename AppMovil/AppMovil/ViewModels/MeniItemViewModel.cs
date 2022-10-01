using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using Prism.Navigation;
using Sindicato.Common;

namespace AppMovil.ViewModels
{
    public class MeniItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _SelectMenuCommand;

        public MeniItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand SelectMenuCommand => _SelectMenuCommand ?? (_SelectMenuCommand = new DelegateCommand(SelectMenuAsync));
        private async void SelectMenuAsync()
        {
            await _navigationService.NavigateAsync($"/SindicatoMasterDetailPage/NavigationPage/{PagNavigation}");
        }
    }
}
