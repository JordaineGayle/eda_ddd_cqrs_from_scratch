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
    public class ReadDbController : BaseController<ReadDbController>
    {
        public ReadDbController(ILogger<ReadDbController> logger, ICommandDispatcher commandDispatcher) : base(logger, commandDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RestoreReadDbAsync(RestoreReadDbCommand command)
        {
            try
            {
                await _commandDispatcher.SendAsync(command);
                return StatusCode(StatusCodes.Status201Created, new BaseResponse("Restore read db completed successfully!"));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Client made a bad request!", new
                {
                    Message = ex.Message
                });

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse(ex.Message));
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to restore read db";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
            }
        }
    }
}
