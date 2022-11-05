using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.prism.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Sindicato.prism.ViewModels
{
    public class ResarvasPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _verViajeCommand;

        public ResarvasPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Reservas de Pasaje";
            _navigationService = navigationService;
        }
        public DelegateCommand VerVaiejCommand => _verViajeCommand ?? (_verViajeCommand = new DelegateCommand(VerTripAsync));
        private async void VerTripAsync()
        {
            await _navigationService.NavigateAsync(nameof(VerRutaPage));
        }
    }
}
