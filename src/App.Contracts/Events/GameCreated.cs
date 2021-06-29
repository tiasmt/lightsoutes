using App.Contracts.Interfaces;
using App.Contracts.Models;

namespace App.Contracts.Events
{
    public record GameCreated : IEvent
    {
        public GameCreated(string gameName, int boardSize, LightsOn lightsOn, bool isActive)
        {
            GameName = gameName;
            BoardSize = boardSize;
            LightsOn = lightsOn;
            IsActive = isActive;
        }
        public string GameName { get; }

        public int BoardSize { get; }
        public LightsOn LightsOn { get; }
        public bool IsActive { get; }
        public string EventType { get; } = nameof(GameCreated);

    }
}