using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer_SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderName,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderName, message);
        }
    }
}
