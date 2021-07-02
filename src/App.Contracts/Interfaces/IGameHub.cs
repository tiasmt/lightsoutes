using App.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.Interfaces
{
    public interface IGameHub
    {
        Task UpdateGame(Game game);
        Task SendEvent(IEvent evnt);
    }
}
