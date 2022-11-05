using Prism;
using Prism.Ioc;
using Sindicato.common.Services;
using Sindicato.prism.Helpers;
using Sindicato.prism.ViewModels;
using Sindicato.prism.Views;
using Syncfusion.Licensing;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Sindicato.prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("NzMxMzY3QDMxMzcyZTM0MmUzME0wdmNNSzdqem0xT3VuWkxxSWE0RERpMkZVdFRkSGx6b1dsOFA4N0NqUWs9");
            InitializeComponent();

            await NavigationService.NavigateAsync("/SindicatoMasterDetailPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<SindicatoMasterDetailPage, SindicatoMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ResarvasPage, ResarvasPageViewModel>();
            containerRegistry.RegisterForNavigation<HorariosPage, HorariosPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.Register<IGeolocationService, GeolocationService>();
            containerRegistry.RegisterForNavigation<RutasPage, RutasPageViewModel>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<StartTripPage, StartTripPageViewModel>();
            containerRegistry.RegisterForNavigation<MyRutasDetailPage, MyRutasDetailPageViewModel>();
            containerRegistry.Register<ILocatorCliente, LocatorCliente>();
            containerRegistry.RegisterForNavigation<MessagePage, MessagePageViewModel>();
            containerRegistry.Register<ISignalService, SignalRService>();
            containerRegistry.RegisterForNavigation<EndTrip, EndTripViewModel>();
            containerRegistry.RegisterForNavigation<VerRutaPage, VerRutaPageViewModel>();
        }
    }
}
