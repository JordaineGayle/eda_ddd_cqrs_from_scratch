using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands
{
    public record EditMessageCommand : BaseCommand
    {
        public string? Message { get; init; }
    }
}
