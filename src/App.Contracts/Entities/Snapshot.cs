using System.Text.Json.Serialization;

namespace App.Contracts.Entities
{
    public class Snapshot
    {
        public long Version { get; set; } = 0;
        public Game State;

        [JsonConstructor]
        public Snapshot(Game state, long version)
        {
            State = state;
            Version = version;
        }

        /*public Snapshot()
        {
            State = new Game();
        }*/
    }
}