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
    public class LikePostController : BaseController<LikePostController>
    {
        public LikePostController(ILogger<LikePostController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> LikePostAsync(Guid id)
        {
            await _commandDispatcher.SendAsync(new LikePostCommand { Id = id});
            return StatusCode(StatusCodes.Status201Created, new BaseResponse("Liked post successfully"));
        }
    }
}
