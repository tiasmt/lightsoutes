using App.Contracts.Interfaces;

namespace App.Contracts.Events
{
    public class GameCreated : IEvent
    {
        public GameCreated(string playerName, string gameName)
        {
            PlayerName = playerName;
            GameName = gameName;
        }
        public string PlayerName { get; }

        public string GameName { get; }

        public string EventType { get; } = nameof(GameCreated);

    }
}