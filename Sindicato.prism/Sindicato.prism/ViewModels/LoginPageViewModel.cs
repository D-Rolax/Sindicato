using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Helpers;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sindicato.prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _LblImei;
        private DelegateCommand _ImeiCommand;
        public LoginPageViewModel(INavigationService navigationService,
           IApiService apiService):base(navigationService)
        {
            Title = "Login";
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
        }
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));
        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));
        public DelegateCommand imeiComand => _ImeiCommand ?? (_ImeiCommand = new DelegateCommand(ObtenerIpAdress));
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un email",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar su contraseña",
                    "Aceptar");
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error de Conccion","No hay conexión a Internet","Aceptar");
                return;
            }

            AuthRequest request = new AuthRequest
            {
                Password = Password,
                Email = Email
            };

            Respuesta response = await _apiService.GetTokenAsync(url, "api", "/User/login", request);

            if (response.Exito==0)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error","Usuario o contraseña incorrecta", "Aceptar");
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Data;
            EmailRequest emailRequest = new EmailRequest
            {
                Email = Email
            };

            Respuesta response2 = await _apiService.GetUserByEmail(url, "api", "/user", "bearer", token.Token, emailRequest);
            List<DatosUsuarioRequest> userResponse = (List<DatosUsuarioRequest>)response2.Data;

            Settings.User = JsonConvert.SerializeObject(userResponse);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/SindicatoMasterDetailPage/NavigationPage/HomePage");
            Password = string.Empty;

        }



        private void RegisterAsync()
        {
            throw new NotImplementedException();
        }

        private void ObtenerIpAdress()
        {
            LblImei = _apiService.GetIpAdress();
        }

        public string LblImei
        {
            get => _LblImei;
            set => SetProperty(ref _LblImei, value);
        }
    }
}
