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
        public async Task SendGame(Game game)
        {
            await Clients.All.UpdateGame(game);
        }
    }
}
