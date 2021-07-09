using App.Contracts.Entities;
using App.Contracts.Events;
using App.Contracts.Interfaces;
using App.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace App.DataLayer.Repository
{
    public class GameEventRepository : IEventRepository
    {

        private readonly GameContext _context;
        //get from config
        private readonly int _snapshotInterval = 5;
        public GameEventRepository(GameContext context)
        {
            _context = context;
        }

        public async Task<IList<IEvent>> GetEvents(string gameName, long start = 0)
        {
            var events = new List<IEvent>();
            var eventData = await _context.Events.Where(x => x.GameName == gameName).Skip((int)start).ToListAsync();
            foreach (var evnt in eventData)
            {
                IEvent resolvedEvent = DeserializeEvent(evnt);
                events.Add(resolvedEvent);
            }
            return events;
        }

        public async Task<IList<IEvent>> GetEventsTill(string gameName, long end)
        {
            var events = new List<IEvent>();
            var eventData = await _context.Events.Where(x => x.GameName == gameName).Take((int)end).ToListAsync();
            foreach (var evnt in eventData)
            {
                IEvent resolvedEvent = DeserializeEvent(evnt);
                events.Add(resolvedEvent);
            }
            return events;
        }

        public async Task<Snapshot> GetSnapshot(string gameName)
        {
            var snapshotEvent = await _context.Snapshots.Where(e => e.GameName == gameName)
                                                        .OrderByDescending(s => s.Id)
                                                        .FirstOrDefaultAsync();
            var snapshot = snapshotEvent == null ? new Snapshot() : JsonConvert.DeserializeObject<Snapshot>(snapshotEvent.Data);
            return snapshot;
        }

        public async Task Save(IList<IEvent> newEvents, Game gameState = null)
        {
            long version = 0;

            foreach (var evnt in newEvents)
            {
                var jsonData = JsonConvert.SerializeObject(evnt);
                var evt = new Event { Timestamp = DateTime.UtcNow, EventType = evnt.EventType, GameName = evnt.GameName, Data = jsonData };
                await _context.Events.AddAsync(evt);
                await _context.SaveChangesAsync();
                version = await _context.Events.Where(e => e.GameName == evnt.GameName).CountAsync();

                if ((version) % _snapshotInterval == 0 && gameState != null)
                {
                    await AppendSnapshot(gameState, (int)version, evnt.GameName);
                }
            }
        }

        private async Task AppendSnapshot(Game game, int version, string gameName)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(new Snapshot { State = game, Version = version });
                await _context.Snapshots.AddAsync(new SnapshotEvent { Version = version, GameName = gameName, EventType = nameof(Snapshot), Timestamp = DateTime.UtcNow, Data = jsonData });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private IEvent DeserializeEvent(Event evnt)
        {
            IEvent result = null;

            if (evnt.EventType == nameof(GameCreated))
            {
                result = JsonConvert.DeserializeObject<GameCreated>(evnt.Data);
            }
            else if (evnt.EventType == nameof(LightToggled))
            {
                result = JsonConvert.DeserializeObject<LightToggled>(evnt.Data);
            }
            return result;
        }
    }
}
