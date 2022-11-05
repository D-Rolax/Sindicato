using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Models;
using Sindicato.common.Models.Response;
using Sindicato.common.Services;
using Sindicato.prism.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sindicato.prism.ViewModels
{
    public class EndTripViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _endTripcommand;
        private Comment _comment;
        private string _comentario;
        private float _qualification;
        private bool _isRunning;
        private ObservableCollection<Comment> _comments;
        public EndTripViewModel(INavigationService navigationService,
            IApiService apiService):base(navigationService)
        {
            Title = "Calificacion";
            _navigationService = navigationService;
            _apiService = apiService;
            Comments = new ObservableCollection<Comment>(CombosHelper.GetComments());
        }
        public DelegateCommand EndTripCommand => _endTripcommand ?? (_endTripcommand = new DelegateCommand(CalificacionAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public string Comentario
        {
            get => _comentario;
            set => SetProperty(ref _comentario, value);
        }
        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }
        public float Qualification
        {
            get => _qualification;
            set => SetProperty(ref _qualification, value);
        }
        private async void CalificacionAsync()
        {
            if (Qualification == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe poner una calificaion", "Aceptar");
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            if (_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Error de conexion", "Aceptar");
                return;
            }
            CalificacionRequest calificacion = new CalificacionRequest()
            {
                IdChofer = 1,
                Puntaje=Qualification,
                Descripcion=Comentario
            };
            await _apiService.AddComentario(url,"api", "/Calificacion",calificacion);
            IsRunning = false;
            await _navigationService.GoBackToRootAsync();
        }
        public Comment Comment
        {
            get => _comment;
            set
            {
                Comment comment = value;
                
                Comentario+= string.IsNullOrEmpty(Comentario) ? $"{comment.Name}" : $", {comment.Name}";
                SetProperty(ref _comment, value);
            }
        }
    }
}
