﻿using Post.Query.Domain.Entities;

namespace Post.Query.Api.Queries.Handlers
{
    public interface IQueryHandler
    {
        Task<List<PostEntity>> HandleAsync(FindAllPostQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostByIdQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostByAuthorQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostWithCommentsQuery query);
        Task<List<PostEntity>> HandleAsync(FindPostWithLikesQuery query);
    }
}
