using App.Contracts.Interfaces;
using App.Contracts.Models;
using System.Collections.Generic;

namespace App.Contracts.Events
{
    public record GameCreated : IEvent
    {
        public GameCreated(string gameName, int boardSize, List<Position> lightsOn, bool isActive)
        {
            GameName = gameName;
            BoardSize = boardSize;
            LightsOn = lightsOn;
            IsActive = isActive;
        }
        public string GameName { get; }

        public int BoardSize { get; }
        public List<Position> LightsOn { get; set; }
        public bool IsActive { get; }
        public string EventType { get; } = nameof(GameCreated);

    }
}