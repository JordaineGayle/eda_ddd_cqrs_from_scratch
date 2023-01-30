using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Query.Api.DTOs;
using Post.Query.Api.Queries;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> logger;
        private readonly IQueryDispatcher<PostEntity> queryDispatcher;

        public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            this.logger = logger;
            this.queryDispatcher = queryDispatcher;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPostAsync() 
        {
            var res = await queryDispatcher.SendAsync(new FindAllPostQuery());
            return HandleResponse(res);
        }

        [HttpGet("byId/{postId}")]
        public async Task<IActionResult> GetByIdAsync(Guid postId)
        {
            var res = await queryDispatcher.SendAsync(new FindPostByIdQuery() { Id = postId });
            return HandleResponse(res);
        }

        [HttpGet("byAuthor/{author}")]
        public async Task<IActionResult> GetByAuthourAsync(string author)
        {
            var res = await queryDispatcher.SendAsync(new FindPostByAuthorQuery() { Author = author });
            return HandleResponse(res);
        }

        [HttpGet("withComments")]
        public async Task<IActionResult> GetWithCommentsAsync()
        {
            var res = await queryDispatcher.SendAsync(new FindPostWithCommentsQuery());
            return HandleResponse(res);
        }

        [HttpGet("withLikes/{numOfLikes}")]
        public async Task<IActionResult> GetWithLikesAsync(int numOfLikes)
        {
            var res = await queryDispatcher.SendAsync(new FindPostWithLikesQuery() { NumOfLikes = numOfLikes });
            return HandleResponse(res);
        }

        private IActionResult HandleResponse(List<PostEntity> res)
        {
            return (res?.Count ?? 0) > 0 ?
                Ok(new PostLookupResponse(res, $"Successfully fetched {res.Count} document(s)"))
                : NoContent();
        }
    }
}
