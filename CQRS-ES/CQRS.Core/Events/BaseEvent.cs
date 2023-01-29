using CQRS.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events
{
    public abstract record BaseEvent : Message
    {
        public int Version { get; init; }

        public string Type { get; init; }

        protected BaseEvent(string type)
        {
            this.Type = type;
        }

        public BaseEvent UpdateVersion(int version) 
        {
            return this with { Version = version };
        }
    }
}
