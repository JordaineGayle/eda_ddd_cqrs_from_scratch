using CQRS.Core.Exceptions;
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
    public class EditMessageController : BaseController<EditMessageController>
    {
        public EditMessageController(ILogger<EditMessageController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMessageAsync(Guid id, EditMessageCommand command)
        {
            try
            {
                await _commandDispatcher.SendAsync(command with { Id = id });

                return Ok(new BaseResponse("Edit message request completed successfully!"));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Client made a bad request!", new
                {
                    Message = ex.Message
                });

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse(ex.Message));
            }
            catch (AggregateNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Unable to retrieve aggregate with the given id = '{id}'", new
                {
                    Message = ex.Message
                });

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse(ex.Message));
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to edit the message of a post!";
                _logger.LogError(ex, "Client made a bad request!", SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
            }
        }
    }
}
