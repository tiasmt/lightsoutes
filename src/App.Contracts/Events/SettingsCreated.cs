using App.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.Events
{
    public record SettingsCreated : IEvent
    {
        public string PlayerName => throw new NotImplementedException();

        public string GameName => throw new NotImplementedException();

        public string EventType { get; } = nameof(SettingsCreated);
    }
}
