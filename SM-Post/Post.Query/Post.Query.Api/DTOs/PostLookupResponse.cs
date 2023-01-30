using Post.Common.DTOs;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.DTOs
{
    public record PostLookupResponse(List<PostEntity> Posts, string message = null);
}
