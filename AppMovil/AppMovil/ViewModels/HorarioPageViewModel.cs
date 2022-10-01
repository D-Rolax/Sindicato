using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMovil.ViewModels
{
    public class HorarioPageViewModel : ViewModelBase
    {
        public HorarioPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Horario de Salidas";
        }
    }
}
