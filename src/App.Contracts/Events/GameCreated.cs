using App.Contracts.Models.Interfaces;

namespace App.Contracts.Models.Events
{
    public class GameCreated : IEvent
    {
        public GameCreated(string playerName, string gameName, string eventType)
        {
            PlayerName = playerName;
            GameName = gameName;
            EventType = eventType;
        }
        public string PlayerName { get; }

        public string GameName { get; }

        public string EventType { get; } = nameof(GameCreated);

    }
}