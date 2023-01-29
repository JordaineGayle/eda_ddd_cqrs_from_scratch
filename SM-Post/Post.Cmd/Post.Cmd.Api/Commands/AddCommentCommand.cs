using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public record AddCommentCommand : BaseCommand
    {
        public string? Comment { get; init; }
        public string? Username { get; init; }
    }
}
