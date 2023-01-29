using CQRS.Core.Domain;
using CQRS.Core.Messages;
using Post.Common.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostAggregate : AggregateRoot
    {
        private bool active;

        private string? author;

        private readonly Dictionary<Guid, Tuple<string?, string?>> comments = new();

        public bool Active { get => active; set => active = value; }

        public PostAggregate() { }

        public PostAggregate(Guid  id, string? author, string? message)
        {
            RaiseEvent(new PostCreatedEvent { Id= id, Author = author, Message=message, DatePosted = DateTimeOffset.UtcNow.ToUniversalTime() });   
        }

        public void Apply(PostCreatedEvent @event)
        {
            _id = @event.Id;
            active = true;
            author = @event.Author;
        }

        public void EditMessage(string? message) 
        {
            if (!active)
            {
                throw new InvalidOperationException("You cannot edit the message of an inactive post!.");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException($"The value of {nameof(message)} cannot be null or empty. Please provide a valid {nameof(message)}");
            }

            RaiseEvent(new MessageUpdatedEvent
            {
                Id = _id,
                Message = message
            });
        }


        public void Apply(MessageUpdatedEvent @event)
        {
            _id = @event.Id;
        }

        public void LikePost()
        {
            if (!active)
            {
                throw new InvalidOperationException("You cannot like an inactive post!.");
            }

            RaiseEvent(new PostLikedEvent { Id = _id });
        }

        public void Apply(PostLikedEvent @event)
        {
            _id = @event.Id;
        }

        public void AddComment(string? comment, string? username)
        {
            if (!active)
            {
                throw new InvalidOperationException("You cannot add comment to an inactive post!.");
            }

            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new InvalidOperationException($"The value of {nameof(comment)} cannot be null or empty. Please provide a valid {nameof(comment)}");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new InvalidOperationException($"The value of {nameof(username)} cannot be null or empty. Please provide a valid {nameof(username)}");
            }

            RaiseEvent(new CommentAddedEvent { Id = _id, CommentId = Guid.NewGuid(), Comment = comment, Username = username, CommentDate = DateTimeOffset.UtcNow.ToUniversalTime() });
        }


        public void Apply(CommentAddedEvent @event) 
        {
            _id = @event.Id;
            comments.Add(@event.CommentId, new Tuple<string?, string?>(@event.Comment, @event.Username));
        }

        public void EditComment(Guid commentId, string? comment, string? username)
        {
            if (!active)
            {
                throw new InvalidOperationException("You cannot edit comment to an inactive post!.");
            }

            if (!comments[commentId]?.Item2?.Equals(username, StringComparison.CurrentCultureIgnoreCase) ?? false)
            {
                throw new InvalidOperationException("You are not allow to edit a comment that was made by another user");
            }

            RaiseEvent(new CommentEditedEvent { Id = _id,Username = username,CommentId = commentId, Comment = comment, EditDate= DateTimeOffset.UtcNow.ToUniversalTime()});
        }

        public void Apply(CommentEditedEvent @event)
        {
            _id= @event.Id;
            comments[@event.CommentId] = new Tuple<string?, string?>(@event.Comment, @event.Username);
        }

        public void RemoveComment(Guid commentId, string? username)
        {
            if (!active)
            {
                throw new InvalidOperationException("You cannot remove comment to an inactive post!.");
            }

            if (!comments[commentId]?.Item2?.Equals(username, StringComparison.CurrentCultureIgnoreCase) ?? false)
            {
                throw new InvalidOperationException("You are not allowed to remove a comment that was made by another user");
            }

            RaiseEvent(new CommentRemovedEvent { Id = _id, CommentId = commentId });
        }


        public void Apply(CommentRemovedEvent @event)
        {
            _id = @event.Id;
            comments.Remove(@event.CommentId);
        }


        public void DeletePost(string? username)
        {
            if (!active)
            {
                throw new InvalidOperationException("You cannot delete an inactive post!.");
            }

            if (!author?.Equals(username, StringComparison.CurrentCultureIgnoreCase) ?? false)
            {
                throw new InvalidOperationException("You are not allowed to delete a post that was made by someone else!");
            }


            RaiseEvent(new PostRemovedEvent { Id = _id });
        }


        public void Apply(PostRemovedEvent @event)
        {
            _id = @event.Id;
            active = false;
        }
    }
}
