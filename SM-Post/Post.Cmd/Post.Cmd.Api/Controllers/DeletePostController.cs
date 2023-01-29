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
    public class DeletePostController : BaseController<DeletePostController>
    {
        public DeletePostController(ILogger<DeletePostController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id, DeletePostCommand command)
        {
            await _commandDispatcher.SendAsync(command with { Id = id });
            return StatusCode(StatusCodes.Status201Created, new BaseResponse("post deleted successfully"));
        }
    }
}
