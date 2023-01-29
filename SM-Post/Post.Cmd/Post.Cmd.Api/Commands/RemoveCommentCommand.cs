using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public record RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; init; }
        public string? Username { get; init; }
    }
}
