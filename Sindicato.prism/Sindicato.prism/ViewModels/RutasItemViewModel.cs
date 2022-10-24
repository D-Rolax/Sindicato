using Prism.Commands;
using Prism.Navigation;
using Sindicato.common.Models.Response;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.prism.ViewModels
{
    public class RutasItemViewModel:RutasRequest
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectRutasCommand;
        public RutasItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand SelectRutasCommand => _selectRutasCommand??(_selectRutasCommand = new DelegateCommand(SelectRutasAsync));

        private async void SelectRutasAsync()
        {
            var parameters = new NavigationParameters
            {
                { "rutas", this }
            };
            await _navigationService.NavigateAsync(nameof(MyRutasDetailPage),parameters);
        }
    }
}
