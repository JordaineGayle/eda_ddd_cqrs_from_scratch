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
    public class EditCommentController : BaseController<EditCommentController>
    {
        public EditCommentController(ILogger<EditCommentController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCommentAsync(Guid id, EditCommentCommand command)
        {
            await _commandDispatcher.SendAsync(command with { Id = id });
            return StatusCode(StatusCodes.Status200OK, new BaseResponse("comment edited successfully"));
        }
    }
}
