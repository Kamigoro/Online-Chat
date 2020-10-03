using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer_SignalR.Hubs
{
    public class GameHub : Hub
    {
        public async Task TakeTurn(int position)
        {
            await Clients.All.SendAsync("ReceivePlayerTurn", position);
        }
    }
}
