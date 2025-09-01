using Forum.Application.DTO.Post.Requests;
using Forum.Application.DTO.Post.Responses;

namespace Forum.Application.Interfaces
{

    public interface IPostHandler
    {
        /// <summary>
        /// Creates a new post with the specified title, content, and user.
        /// </summary>
        /// <param name="title">The title of the post.</param>
        /// <param name="content">The content/body of the post.</param>
        /// <param name="userId">The ID of the user creating the post.</param>
        /// <param name="cancellationToken">Token to observe for cancellation.</param>
        /// <returns>The unique identifier (GUID) of the newly created post.</returns>
        public Task<Guid> CreatePostAsync(string title, string content, Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the details of a specific post by its unique identifier.
        /// </summary>
        /// <param name="postId">The unique identifier of the post to retrieve.</param>
        /// <param name="cancellationToken">Token to observe for cancellation.</param>
        /// <returns>A <see cref="FullPostResponse"/> containing all details of the post.</returns>
        public Task<FullPostResponse> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a paginated list of posts based on filtering, sorting, and pagination parameters.
        /// </summary>
        /// <param name="getPostsQuery">The query object containing filters, sort options, and pagination info.</param>
        /// <param name="cancellationToken">Token to observe for cancellation.</param>
        /// <returns>A <see cref="PaginatedResponse{FullPostResponse}"/> containing a page of posts.</returns>
        public Task<PaginatedResponse<FullPostResponse>> GetPostsAsync(
           GetPostsQuery getPostsQuery, CancellationToken cancellationToken);
    }
}
