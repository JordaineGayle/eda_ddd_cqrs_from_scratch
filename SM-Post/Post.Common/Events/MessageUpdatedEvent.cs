using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Events
{
    public record MessageUpdatedEvent : BaseEvent
    {
        public string? Message { get; init; }

        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent)){}
    }
}
