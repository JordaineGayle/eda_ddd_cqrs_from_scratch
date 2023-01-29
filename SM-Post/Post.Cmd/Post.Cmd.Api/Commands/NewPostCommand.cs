using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public record NewPostCommand : BaseCommand
    {
        public string? Author { get; init; }
        public string? Message { get; init; }
    }
}
