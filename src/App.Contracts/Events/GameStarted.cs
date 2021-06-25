using App.Contracts.Models.Interfaces;

namespace App.Contracts.Events
{
    public class GameStarted : IEvent
    {
        public string PlayerName => throw new System.NotImplementedException();

        public string GameName => throw new System.NotImplementedException();

        public string EventType { get; } = nameof(GameStarted);
    }
}