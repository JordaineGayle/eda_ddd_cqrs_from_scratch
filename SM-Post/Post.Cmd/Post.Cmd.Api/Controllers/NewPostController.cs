using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.Controllers.Base;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewPostController : BaseController<NewPostController>
    {
        public NewPostController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> NewPostAsync(NewPostCommand command)
        {
            var id = Guid.NewGuid();

            try
            {
                await _commandDispatcher.SendAsync(command with { Id = id });
                return StatusCode(StatusCodes.Status201Created, new NewPostResponse("New post creation request completed successfully!", id));
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Client made a bad request!", new 
                {
                    Message = ex.Message
                });

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse(ex.Message));
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new post!";
                _logger.LogError(ex, "Client made a bad request!", SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new NewPostResponse(SAFE_ERROR_MESSAGE, id));
            }
        }
    }
}
