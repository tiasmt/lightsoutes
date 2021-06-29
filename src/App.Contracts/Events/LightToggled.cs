using App.Contracts.Interfaces;

namespace App.Contracts.Events
{
    public record LightToggled : IEvent
    {
        public LightToggled(string gameName, int posX, int posY)
        {
            GameName = gameName;
            PosX = posX;
            PosY = posY;
        }

        public string EventType { get; } = nameof(LightToggled);
        public string GameName { get;}
        public int PosX { get; }
        public int PosY { get; }
    }
}