using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Helpers;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sindicato.prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _startTripCommand;

        public HomePageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Sindicato Señor de Santiago";
            _navigationService = navigationService;
        }
        public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand= new DelegateCommand(StartTripAsync));

        private async void StartTripAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(StartTripPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }
    }
}
