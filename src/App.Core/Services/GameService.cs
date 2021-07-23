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
        private string _connectionId = String.Empty;

        public int Version { get; protected set; }
        private Game _gameState;

        public GameService(IEventRepository repository, IHubContext<GameHub, IGameHub> gameHub, ILogger<IGameService> logger)
        {
            _repository = repository;
            _gameHub = gameHub;
            _logger = logger;
        }

        public async Task CreateGame(string connectionId, int boardSize, string gameName, string playerName)
        {
            try
            {
                if (boardSize <= 0)
                    throw new Exception("Invalid grid size");

                _connectionId = connectionId;
                var lightsOn = CreateBoard(boardSize: boardSize);
                await ApplyEvent(new GameCreated(gameName: gameName, boardSize: boardSize, lightsOn: lightsOn, isActive: true));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        public async Task ToggleLight(string connectionId, string gameName, int x, int y)
        {
            try
            {
                _connectionId = connectionId;
                await ApplyEvent(new LightToggled(gameName: gameName, posX: x, posY: y));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }

        public async Task Replay(string connectionId, string gameName, int end)
        {
            try
            {
                _connectionId = connectionId;
                await ApplyEvent(new EmptyEvent( gameName: gameName), end: end);
            }
            catch (Exception ex)
            { 
                _logger.Log(LogLevel.Error, ex.Message);
            }

        }

        public List<Position> CreateBoard(int boardSize)
        {
            var rnd = new Random();
            var numberOfLightsOn = rnd.Next(1, boardSize);
            var lightsOn = new List<Position>();
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

        private List<Position> Toggle(List<Position> lights, int x, int y)
        {
            if (lights.Exists(l => (l.X == x && l.Y == y)))
                lights.RemoveAll(l => (l.X == x && l.Y == y));
            else
                lights.Add(new Position(x, y));

            return lights;
        }

        private bool IsGameActive(List<Position> lightsOn)
        {
            var result = lightsOn.Count > 0 ? true : false;
            return result;
        }

        public IList<IEvent> GetEvents()
        {
            return _events;
        }

        public async Task ApplyEvent(IEvent evnt, bool isFastForward = false, int end = 0, bool isReplay = false)
        {
            if (_gameState == null)
                await GetGame(evnt.GameName, end);

            switch (evnt)
            {
                case GameCreated gameCreated:
                    Apply(gameCreated);
                    break;
                case LightToggled lightToggled:
                    Apply(lightToggled);
                    break;
            }

            if (isFastForward == false && evnt.EventType != nameof(EmptyEvent))
            {
                if (!isReplay)
                {
                    _uncommittedevents.Add(evnt);
                }
                await _gameHub.Clients.Client(_connectionId).UpdateGame(_gameState);
            }
            else
            {
                _events.Add(evnt);
            }
           
            if (evnt.EventType != nameof(EmptyEvent))
            {
                var uncommittedEvents = GetUncommittedEvents();
                await _repository.Save(uncommittedEvents, _gameState);
                await _gameHub.Clients.Client(_connectionId).SendEvent(evnt);
            }
        }

        public IList<IEvent> GetUncommittedEvents()
        {
            var uncommittedEvents = _uncommittedevents.ToArray();
            _uncommittedevents.Clear();
            return uncommittedEvents;
        }

        private void Apply(GameCreated gameCreated)
        {
            _gameState = new Game(lightsOn: gameCreated.LightsOn, isActive: true, boardSize: gameCreated.BoardSize);
        }


        private void Apply(LightToggled lightToggled)
        {
            var lightsOn = ToggleLights(_gameState.LightsOn, lightToggled.PosX, lightToggled.PosY, _gameState.BoardSize);
            _gameState = new Game(lightsOn: lightsOn, isActive: _gameState.IsActive, boardSize: _gameState.BoardSize);

        }

        public List<Position> ToggleLights(List<Position> lights, int x, int y, int size)
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

        private async Task GetGame(string gameName, int end = 0)
        {
            try
            {
                var isFastForward = end == 0;
                var snapshot = await _repository.GetSnapshot(gameName);
                _gameState = snapshot.State;
                var events = end == 0 ? await _repository.GetEvents(gameName, snapshot.Version) : await _repository.GetEventsTill(gameName, end);
                foreach (var evnt in events)
                {
                    await ApplyEvent(evnt, isFastForward, isReplay: !isFastForward);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }
    }
}