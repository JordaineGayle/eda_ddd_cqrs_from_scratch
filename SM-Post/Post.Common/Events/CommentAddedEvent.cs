using CQRS.Core.Events;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Events
{
    public record CommentAddedEvent : BaseEvent
    {
        public Guid CommentId { get; init; }
        public string? Comment { get; init; }
        public string? Username { get; init; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public DateTimeOffset CommentDate { get; init; }

        public CommentAddedEvent() : base(nameof(CommentAddedEvent))
        {
        }
    }
}
