using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Entities;
using App.Contracts.Events;
using App.Contracts.Interfaces;
using App.Contracts.Repository;

namespace App.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IList<IEvent> _events = new List<IEvent>();
        private readonly IList<IEvent> _uncommittedevents = new List<IEvent>();
        private readonly IEventRepository _repository;
        public int Version { get; protected set; }
        private Game _gameState;

        public GameService(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateGame(int size, string gameName, string playerName)
        {
            //TODO: Add logic
            await ApplyEvent(new GameCreated(playerName, gameName)); ;
        }

        public async Task StartGame()
        {
            //TODO: Add logic
            await ApplyEvent(new GameStarted());
        }

        public async Task ToggleLight(int gameId, int x, int y)
        {
            //TODO: Add logic
            await ApplyEvent(new LightToggled());
        }

        public IList<IEvent> GetEvents()
        {
            return _events;
        }

        public async Task ApplyEvent(IEvent evnt, bool isFastForward = false)
        {
            if (_gameState == null)
                await GetGame(evnt.GameName);

            switch (evnt)
            {
                case GameCreated gameCreated:
                    Apply(gameCreated);
                    break;
                case LightToggled lightToggled:
                    Apply(lightToggled);
                    break;
                case GameStarted gameStarted:
                    Apply(gameStarted);
                    break;
            }

            if (isFastForward == false)
            {
                _uncommittedevents.Add(evnt);
                //await _portfolioHub.Clients.All.UpdatePortfolio(_portfolioState);
            }
            else
            {
                _events.Add(evnt);
            }

            var uncommittedEvents = GetUncommittedEvents();
            await _repository.Save(uncommittedEvents, _gameState);

        }

        public IList<IEvent> GetUncommittedEvents()
        {
            var uncommittedEvents = _uncommittedevents.ToArray();
            _uncommittedevents.Clear();
            return uncommittedEvents;
        }

        private void Apply(GameCreated gameCreated)
        {          
        }


        private void Apply(LightToggled lightToggled)
        { }

        private void Apply(GameStarted gameStarted)
        { }

        public async Task<Game> GetState(string gameName)
        {
            await GetGame(gameName);
            return _gameState;
        }

        private async Task GetGame(string gameName)
        {
            var snapshot = await _repository.GetSnapshot(gameName);
            _gameState = snapshot.State;
            var events = await _repository.GetEvents(gameName, snapshot.Version);
            foreach (var evnt in events)
            {
                await ApplyEvent(evnt, true);
            }
        }
    }
}