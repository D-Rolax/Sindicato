using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMovil.ViewModels
{
    public class ReservasPageViewModel : ViewModelBase
    {
        public ReservasPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Reservas de pasaje";
        }
    }
}
