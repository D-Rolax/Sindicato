using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WSSindicato.Hubs
{
    public class TrakingHub:Hub
    {
        private static Dictionary<int, string> _deviceConnection;
        private static Dictionary<string, int> _connectionDevice;
        public TrakingHub()
        {
            _deviceConnection = _deviceConnection ?? new Dictionary<int, string>();
            _connectionDevice = _connectionDevice ?? new Dictionary<string, int>();
        }
        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("SignalR server connection");
            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            int? deviceId = _connectionDevice.ContainsKey(Context.ConnectionId) ?
                (int?)_connectionDevice[Context.ConnectionId] : null;
            if (deviceId.HasValue)
            {
                _deviceConnection.Remove(deviceId.Value);
                _connectionDevice.Remove(Context.ConnectionId);
            }
            Debug.WriteLine($"SignaIR server disconected. Device: {deviceId}");
            await base.OnDisconnectedAsync(exception);
        }
        [HubMethodName("Init")]
        public Task Init(DeviceInfo info)
        {
            _deviceConnection.Add(info.Id, Context.ConnectionId);
            _connectionDevice.Add(Context.ConnectionId,info.Id);
            return Task.CompletedTask;
        }
        [HubMethodName("SendMessageToAll")]
        public async Task SendMessageToall(MessageItem item)
        {
            await Clients.All.SendAsync("NewMessage",item);
        }
        [HubMethodName("SendMessageToDevice")]
        public async Task SendMessageToDivice(MessageItem item)
        {
            Debug.WriteLine($"SignalR send message{item.Message} from {item.SourceId} to {item.TargetId}");
            if (_deviceConnection.ContainsKey(item.TargetId))
            {
                await Clients.Client(_deviceConnection[item.TargetId]).SendAsync("NewMessage",item);
            }
        }
    }
}
