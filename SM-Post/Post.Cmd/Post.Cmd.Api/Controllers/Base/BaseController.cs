using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Post.Cmd.Api.Controllers.Base
{
    public class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly ICommandDispatcher _commandDispatcher;

        public BaseController(ILogger<T> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }
    }
}
