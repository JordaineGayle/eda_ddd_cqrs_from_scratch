using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Events
{
    public record CommentRemovedEvent : BaseEvent
    {
        public Guid CommentId { get; init; }

        public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
        {
        }
    }
}
