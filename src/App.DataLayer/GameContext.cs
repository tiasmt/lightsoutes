using App.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.DataLayer
{
    public class GameContext : DbContext
    {
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
        { 
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<SnapshotEvent> Snapshots { get; set; }

    }
}
