using AppMovil.ViewModels;
using AppMovil.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace AppMovil
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("SindicatoMasterDetailPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SindicatoMasterDetailPage, SindicatoMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ReservasPage, ReservasPageViewModel>();
            containerRegistry.RegisterForNavigation<HorarioPage, HorarioPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
