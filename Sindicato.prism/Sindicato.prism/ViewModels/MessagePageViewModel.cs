using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sindicato.common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using WSSindicato.Hubs;

namespace Sindicato.prism.ViewModels
{
    public class MessagePageViewModel : ViewModelBase
    {
        private readonly ISignalService _signalService;
        private DelegateCommand _connectComman;
        private DelegateCommand _sendToComman;
        private DelegateCommand _sendDeviceComman;
        private string _menssage;
        private int _enId;
        private int _endTarget;
        private string _lbState;
        public MessagePageViewModel(INavigationService navigationService,
            ISignalService signalService):base(navigationService)
        {
            Title = "Mensajes";
            _signalService = signalService;
            _signalService.MessageReceived += _signalService_MessageReceived;
            _signalService.Connecting += _signalService_Connecting;
            _signalService.Connected += _signalService_Connected;
        }

        private void _signalService_Connected(object sender, EventArgs e)
        {
            Lbstate = "Conected";
        }

        private void _signalService_Connecting(object sender, EventArgs e)
        {
            Lbstate = "Connection...";
        }

        private async void _signalService_MessageReceived(object sender, MessageItem e)
        {
            await App.Current.MainPage.DisplayAlert("Message reciveid", e.Message, "Ok");
        }

        public DelegateCommand ConnectCommand=>_connectComman??(_connectComman = new DelegateCommand(ConnectandoAsync));
        public DelegateCommand SendToCommand => _sendToComman ?? (_sendToComman = new DelegateCommand(SendToAllAsync));
        public DelegateCommand SendDevice => _sendDeviceComman ?? (_sendDeviceComman = new DelegateCommand(SendDeviceMessage));
        public string SendMessage {
            get => _menssage;
            set => SetProperty(ref _menssage,value); 
        }
        public string Lbstate
        {
            get => _lbState;
            set => SetProperty(ref _lbState, value);
        }
        public int EnId
        {
            get => _enId;
            set => SetProperty(ref _enId, value);
        }
        public int EndTargtId
        {
            get => _endTarget;
            set => SetProperty(ref _endTarget, value);
        }
        private void SendDeviceMessage()
        {
            _signalService.SendMessageToDevice(new MessageItem
            {
                Message = SendMessage,
                SourceId = Convert.ToInt32(EnId),
                TargetId = Convert.ToInt32(EndTargtId)
            });
        }

        private void SendToAllAsync()
        {
            _signalService.SendMessageToAll(new MessageItem
            {
                Message = SendMessage,
                SourceId = Convert.ToInt32(EnId),
                //TargetId = Convert.ToInt32(enTargetId.Text)
            });
        }

        private void ConnectandoAsync()
        {
            SignalRService.DeviceId = Convert.ToInt32(EnId);
            _signalService.StartWidhReconnectionAsync();
        }
    }
}
