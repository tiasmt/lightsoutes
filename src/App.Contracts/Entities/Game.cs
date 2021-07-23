using App.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Contracts.Entities
{
    public record Game
    {
        [JsonConstructor]
        public Game(bool isActive = false, List<Position> lightsOn = null, int boardSize = 0)
        {
            IsActive = isActive;
            LightsOn = lightsOn;
            BoardSize = boardSize;
        }

        public int BoardSize { get; }    
        public bool IsActive { get; }
        public List<Position> LightsOn { get;}

    }
}
