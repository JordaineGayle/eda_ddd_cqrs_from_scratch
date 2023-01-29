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
    public class AddCommentController : BaseController<AddCommentController>
    {
        public AddCommentController(ILogger<AddCommentController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddCommentAsync(Guid id, AddCommentCommand command)
        {
            await _commandDispatcher.SendAsync(command with { Id = id});
            return StatusCode(StatusCodes.Status201Created, new BaseResponse("comment created successfully"));
        }
    }
}
