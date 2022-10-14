using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sindicato.prism.ViewModels
{
    public class ResarvasPageViewModel : ViewModelBase
    {
        public ResarvasPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Reservas de Pasaje";
        }
    }
}
