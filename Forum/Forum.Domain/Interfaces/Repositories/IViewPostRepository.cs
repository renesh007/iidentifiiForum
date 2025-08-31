using Forum.Domain.Entities;
using Forum.Domain.Models;

namespace Forum.Domain.Interfaces.Repositories
{
    public interface IViewPostRepository
    {
        /// <summary>
        /// Retrieves a complete view of a post, including its associated details, based on the specified post ID.
        /// </summary>
        /// <param name="postId">The unique identifier of the post to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PostView"/> object
        /// representing the full details of the post if found; otherwise, <see langword="null"/>.</returns>
        public Task<PostView?> GetFullPostByIdAsync(Guid postId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a paginated list of posts along with the total count of posts matching the specified criteria.
        /// </summary>
        /// <remarks>This method supports pagination, filtering, and sorting to allow efficient retrieval
        /// of posts based on the specified criteria. If the specified page number exceeds the total number of pages,
        /// the returned collection will be empty.</remarks>
        /// <param name="pageNumber">The page number to retrieve. Must be greater than or equal to 1.</param>
        /// <param name="pageSize">The number of posts to include in each page. Must be greater than 0.</param>
        /// <param name="filterOptions">Optional filtering criteria to apply to the posts. If null, no filtering is applied.</param>
        /// <param name="sortingDirection">Optional sorting direction to apply to the posts. If null, the default sorting direction is used.</param>
        /// <param name="sortingOptions">Optional sorting criteria to determine the order of the posts. If null, the default sorting criteria is
        /// used.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation will terminate early if the token is canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is a tuple containing: <list
        /// type="bullet"> <item> <description>An <see cref="IEnumerable{T}"/> of <see cref="PostView"/> representing
        /// the posts in the requested page.</description> </item> <item> <description>An <see cref="int"/> representing
        /// the total count of posts matching the specified criteria.</description> </item> </list></returns>
        public Task<(IEnumerable<PostView>, int TotalCount)> GetPostsAsync(
        int pageNumber,
        int pageSize,
        FilterOptions? filterOptions,
        SortingDirection? sortingDirection,
        SortingOptions? sortingOptions,
        CancellationToken cancellationToken);
    }
}
