using App.Contracts.Entities;

namespace App.DataLayer.Entities
{
    public class Snapshot : Contracts.Entities.Snapshot
    {
        public Snapshot(Game state, long version) : base(state, version)
        {
            State = state;
            Version = version;
        }
    }

}
