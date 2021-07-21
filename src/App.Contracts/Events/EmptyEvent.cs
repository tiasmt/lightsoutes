using App.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.Events
{
    public class EmptyEvent : IEvent
    {

        public EmptyEvent(string gameName)
        {
            GameName = gameName; 
        }
        public string GameName { get; }

        public string EventType { get; }  = nameof(EmptyEvent);
    }
}
