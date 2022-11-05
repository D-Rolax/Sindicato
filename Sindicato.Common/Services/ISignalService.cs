using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Hubs;

namespace Sindicato.common.Services
{
    public interface ISignalService
    {
        event EventHandler<MessageItem> MessageReceived;
        event EventHandler Connecting;
        event EventHandler Connected;
        void StartWidhReconnectionAsync();
        Task SendMessageToAll(MessageItem item);
        Task SendMessageToDevice(MessageItem item);
        Task StopAsync();
    }
}
