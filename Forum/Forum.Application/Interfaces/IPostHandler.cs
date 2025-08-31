using Forum.Application.DTO.Post.Responses;
using Forum.Domain.Models;

namespace Forum.Application.Interfaces
{

    public interface IPostHandler
    {
        public Task<Guid> CreatePostAsync(string title, string content, Guid userId, CancellationToken cancellationToken);
        public Task<FullPostResponse> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken);
        public Task<PaginatedResponse<FullPostResponse>> GetPostsAsync(
            CancellationToken cancellationToken,
            FilterOptions? filterOptions,
            SortingDirection? sortingDirection,
            SortingOptions? sortingOptions,
            int pageNumber,
            int pageSize);
    }
}
