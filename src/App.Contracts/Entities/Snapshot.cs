namespace App.Contracts.Entities
{
    public class Snapshot
    {
        public long Version { get; set; } = 0;
        public Game State;
        public Snapshot()
        {
            State = new Game();
        }
    }
}