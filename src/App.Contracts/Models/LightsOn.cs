using System.Collections.Generic;

namespace App.Contracts.Models
{
    //State
    public class LightsOn
    {
        public LightsOn()
        {
            On = On?? new List<Position>();
        }

        public LightsOn(int gameId, List<Position> positions)
        {
            GameId = gameId;
            On = positions;
        }
        public List<Position> On {get;set;}
        public int Id {get;set;}
        public int GameId {get;set;}
    }
}