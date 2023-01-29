using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public record EditCommentCommand : BaseCommand
    {
        public Guid CommentId { get; init; } 
        public string? Comment { get; init; }
        public string? Username { get; init; }
    }
}
