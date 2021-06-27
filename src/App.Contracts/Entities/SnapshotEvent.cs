using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Contracts.Entities
{
    public class SnapshotEvent : Event
    {
        public long Version { get; set; }
    }
}
