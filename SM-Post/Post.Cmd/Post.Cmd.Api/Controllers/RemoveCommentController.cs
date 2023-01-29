using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Controllers.Base;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveCommentController : BaseController<RemoveCommentController>
    {
        public RemoveCommentController(ILogger<RemoveCommentController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCommentAsync(Guid id, RemoveCommentCommand command)
        {
            await _commandDispatcher.SendAsync(command with { Id = id });
            return StatusCode(StatusCodes.Status200OK, new BaseResponse("comment deleted successfully"));
        }
    }
}
