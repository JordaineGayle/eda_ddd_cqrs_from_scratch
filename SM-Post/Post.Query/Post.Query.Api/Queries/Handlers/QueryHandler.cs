using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.Api.Queries.Handlers
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IPostRepository _postRepository;

        public QueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<List<PostEntity>> HandleAsync(FindAllPostQuery query) => _postRepository.ListAllAsync();

        public Task<List<PostEntity>> HandleAsync(FindPostByIdQuery query) => ToListAsync(() => _postRepository.GetByIdAsync(query.Id));

        public Task<List<PostEntity>> HandleAsync(FindPostByAuthorQuery query) => _postRepository.ListByAuthorAsync(query.Author);

        public Task<List<PostEntity>> HandleAsync(FindPostWithCommentsQuery query) => _postRepository.ListWithCommentsAsync();

        public Task<List<PostEntity>> HandleAsync(FindPostWithLikesQuery query) => _postRepository.ListWithLikesAsync(query.NumOfLikes);

        private async Task<List<R>> ToListAsync<R>(Func<Task<R>> method)
        {
            var list = new List<R>();
            var data = await method();
            list.Add(data);
            return list;
        }
    }
}
