using Sindicato.common.Services;
using System;
using WSSindicato.Hubs;
using Xamarin.Forms;

namespace Sindicato.prism.Views
{
    public partial class MessagePage : ContentPage
    {
        private readonly ISignalService _signalService;

        public MessagePage(ISignalService signalService)
        {
            //_signalService = DependencyService.Get<ISignalService>();
            InitializeComponent();
            _signalService = signalService;
        }
    }
}
