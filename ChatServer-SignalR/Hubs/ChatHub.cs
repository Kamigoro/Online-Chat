using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer_SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string username,string text)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, text);
        }
    }
}
