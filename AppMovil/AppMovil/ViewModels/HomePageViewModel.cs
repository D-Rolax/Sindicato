using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMovil.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService):
            base(navigationService)
        {
            Title = "Sindicato Señor de Santiago";
        }
    }
}
