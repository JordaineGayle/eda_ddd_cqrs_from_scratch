using CQRS.Core.Events;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Events
{
    public record CommentEditedEvent : BaseEvent
    {
        public Guid CommentId { get;init; } 
        public string? Comment { get;init; }
        public string? Username { get;init; }
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public DateTimeOffset EditDate { get;init; }

        public CommentEditedEvent() : base(nameof(CommentEditedEvent))
        {
        }
    }
}
