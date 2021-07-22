using App.Contracts.Entities;
using App.Contracts.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Hubs
{
    public class GameHub : Hub<IGameHub>
    {

        public async override Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).Connected(Context.ConnectionId);
        }
        public async Task SendGame(string gameName, Game game)
        {
            await Clients.User(gameName).UpdateGame(game);
        }

        public async Task SentLatestEvent(IEvent evnt)
        {
            await Clients.User(evnt.GameName).SendEvent(evnt);
        }
    }
}
