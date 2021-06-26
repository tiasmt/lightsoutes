using System.Collections.Generic;
using System.Threading.Tasks;
using App.Contracts.Entities;
using App.Contracts.Interfaces;

namespace App.Contracts.Repository
{
    public interface IEventRepository
    {
        Task Save(IList<IEvent> newEvents, Game gameState = null);
        Task<IList<IEvent>> GetEvents(string username, long start = 0);
        Task<Snapshot> GetSnapshot(string username);
    }
}