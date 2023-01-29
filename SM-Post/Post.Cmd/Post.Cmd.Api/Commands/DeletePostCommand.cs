using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public record DeletePostCommand : BaseCommand
    {
        public string? Username { get; init; }
    }
}
