using App.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.Entities
{
    public record Game
    {
        public Game()
        { 
        
        }

        public Game(bool isActive, LightsOn lightsOn, int boardSize)
        {
            IsActive = isActive;
            LightsOn = lightsOn;
            BoardSize = boardSize;
        }

        public int BoardSize { get; set; }    
        public bool IsActive { get; set; }
        public LightsOn LightsOn { get; set; }
    }
}
