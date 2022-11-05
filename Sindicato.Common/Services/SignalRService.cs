using Microsoft.AspNetCore.SignalR.Client;
using Sindicato.common.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Hubs;

namespace Sindicato.common.Services
{
    public class SignalRService : ISignalService
    {
        public event EventHandler<MessageItem> MessageReceived;
        public event EventHandler Connecting;
        public event EventHandler Connected;
        private const string INIT_OPERATION = "Init";
        private const string SEND_MESSAGE_TO_DEVICE_OPERATION = "SendMessageToDevice";
        private const string SEND_MESSAGE_TO_ALL_OPERATION = "SendMessageToAll";
        public static int DeviceId { get; set; }
        private HubConnection _connection;
        public SignalRService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://sindicatoservice.azurewebsites.net/trakingHub")
                .WithAutomaticReconnect(new SignalRretryPolicy())
                .Build();
        }
        public async void StartWidhReconnectionAsync()
        {
            if (_connection.State!=HubConnectionState.Disconnected)
            {
                return;
            }
            Connecting.Invoke(this, null);
            _connection.Closed += _connection_Closed;
            _connection.Reconnected += _connection_Reconnected;

            _connection.On<MessageItem>("NewMessage",NewMessage);
            while (true)
            {
                try
                {
                    await _connection.StartAsync();
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"SignalR error {ex.Message}");
                    await Task.Delay(3000);
                }
            }
            Debug.WriteLine($"SignalR connected");
            await Init();
            Connected?.Invoke(this,null);
        }

        private async Task _connection_Reconnected(string arg)
        {
            await Init();
            Connected?.Invoke(this, null);
        }



        private Task _connection_Closed(Exception arg)
        {
            StartWidhReconnectionAsync();
            return Task.CompletedTask;
        }
        private async Task Init()
        {
            try
            {
                await _connection.InvokeAsync(INIT_OPERATION,new DeviceInfo { Id=DeviceId});
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void NewMessage(MessageItem obj)
        {
            MessageReceived?.Invoke(this, obj);
        }
        public async Task SendMessageToAll(MessageItem item)
        {
            try
            {
                await _connection.InvokeAsync(SEND_MESSAGE_TO_ALL_OPERATION,item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendMessageToDevice(MessageItem item)
        {
            try
            {
                await _connection.InvokeAsync(SEND_MESSAGE_TO_DEVICE_OPERATION, item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task StopAsync()
        {
            if (_connection.State==HubConnectionState.Disconnected)
            {
                return;
            }
            await _connection.StopAsync();
            _connection.Closed -= _connection_Closed;
            _connection.Reconnected -= _connection_Reconnected;
        }
    }
}
