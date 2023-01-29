using CQRS.Core.Events;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Events
{
    public record PostCreatedEvent : BaseEvent
    {
        public string? Author { get; init; }
        public string? Message { get; init; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public DateTimeOffset DatePosted { get; init; }

        public PostCreatedEvent() : base(nameof(PostCreatedEvent)) { }


    }
}
