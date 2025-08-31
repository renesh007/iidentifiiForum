using Forum.Application.DTO.Post.Requests;
using Forum.Application.DTO.Post.Responses;

namespace Forum.Application.Interfaces
{

    public interface IPostHandler
    {
        public Task<Guid> CreatePostAsync(string title, string content, Guid userId, CancellationToken cancellationToken);
        public Task<FullPostResponse> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken);
        public Task<PaginatedResponse<FullPostResponse>> GetPostsAsync(
           GetPostsQuery getPostsQuery, CancellationToken cancellationToken);
    }
}
