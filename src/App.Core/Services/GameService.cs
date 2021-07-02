using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.Entities;
using App.Contracts.Events;
using App.Contracts.Interfaces;
using App.Contracts.Models;
using App.Contracts.Repository;
using App.Core.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace App.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IList<IEvent> _events = new List<IEvent>();
        private readonly IList<IEvent> _uncommittedevents = new List<IEvent>();
        private readonly IEventRepository _repository;
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly ILogger<IGameService> _logger;

        public int Version { get; protected set; }
        private Game _gameState;

        public GameService(IEventRepository repository, IHubContext<GameHub, IGameHub> gameHub, ILogger<IGameService> logger)
        {
            _repository = repository;
            _gameHub = gameHub;
            _logger = logger;
        }

        public async Task CreateGame(int boardSize, string gameName, string playerName)
        {
            try
            {
                if (boardSize <= 0)
                    throw new Exception("Invalid grid size");

                var lightsOn = CreateBoard(boardSize: boardSize);
                await ApplyEvent(new GameCreated(gameName: gameName, boardSize: boardSize, lightsOn: lightsOn, isActive: true));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        public async Task ToggleLight(string gameName, int x, int y)
        {
            try
            {
                await ApplyEvent(new LightToggled(gameName: gameName, posX: x, posY: y));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        public LightsOn CreateBoard(int boardSize)
        {
            var rnd = new Random();
            var numberOfLightsOn = rnd.Next(1, boardSize);
            var lightsOn = new LightsOn();
            for (var i = 0; i < numberOfLightsOn; i++)
            {
                int x = rnd.Next(0, boardSize - 1);
                int y = rnd.Next(0, boardSize - 1);
                Toggle(lightsOn, x, y);
            }

            //Edge case where after initializing the game no lights are ON we force 1 to be ON
            if (!IsGameActive(lightsOn))
                Toggle(lightsOn, 0, 0);

            return lightsOn;
        }

        private LightsOn Toggle(LightsOn lights, int x, int y)
        {
            if (lights.On.Exists(l => (l.X == x && l.Y == y)))
                lights.On.RemoveAll(l => (l.X == x && l.Y == y));
            else
                lights.On.Add(new Position(x, y));

            return lights;
        }

        private bool IsGameActive(LightsOn lightsOn)
        {
            var result = lightsOn.On.Count > 0 ? true : false;
            return result;
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
            }

            if (isFastForward == false)
            {
                _uncommittedevents.Add(evnt);
                await _gameHub.Clients.All.UpdateGame(_gameState);
            }
            else
            {
                _events.Add(evnt);
            }
            await _gameHub.Clients.All.SendEvent(evnt);
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
            _gameState.IsActive = gameCreated.IsActive;
            _gameState.LightsOn = gameCreated.LightsOn;
            _gameState.BoardSize = gameCreated.BoardSize;
        }


        private void Apply(LightToggled lightToggled)
        {
            _gameState.LightsOn = ToggleLights(_gameState.LightsOn, lightToggled.PosX, lightToggled.PosY, _gameState.BoardSize);

        }

        public LightsOn ToggleLights(LightsOn lights, int x, int y, int size)
        {

            if (x >= size || y >= size || x < 0 || y < 0)
                throw new Exception("Incorrect position specified");

            Toggle(lights, x, y);

            if (x > 0)
                Toggle(lights, x - 1, y);
            if (y > 0)
                Toggle(lights, x, y - 1);
            if (x < size - 1)
                Toggle(lights, x + 1, y);
            if (y < size - 1)
                Toggle(lights, x, y + 1);

            return lights;
        }

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