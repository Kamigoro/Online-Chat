using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WPFChatClient.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection hubConnection;

        public event Action<string, string> MessageReceivedEvent;

        public SignalRChatService(HubConnection connection)
        {
            hubConnection = connection;
            hubConnection.On<string, string>("ReceiveMessage", (senderName,message) => MessageReceivedEvent?.Invoke(senderName, message));
        }

        public async Task Connect()
        {
            await hubConnection.StartAsync();
        }

        public async Task SendMessage(string sendername, string message)
        {
            await hubConnection.SendAsync("SendMessage", sendername, message);
        }

    }
}
