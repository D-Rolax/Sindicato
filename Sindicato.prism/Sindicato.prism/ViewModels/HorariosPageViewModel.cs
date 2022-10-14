using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sindicato.prism.ViewModels
{
    public class HorariosPageViewModel : ViewModelBase
    {
        public HorariosPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Horarios de salida";
        }
    }
}
