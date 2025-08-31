using Forum.Domain.Entities;
using Forum.Domain.Models;

namespace Forum.Domain.Interfaces.Repositories
{
    public interface IViewPostRepository
    {
        public Task<PostView?> GetFullPostByIdAsync(Guid postId, CancellationToken cancellationToken);

        public Task<(IEnumerable<PostView>, int TotalCount)> GetPostsAsync(
        int pageNumber,
        int pageSize,
        FilterOptions? filterOptions,
        SortingDirection? sortingDirection,
        SortingOptions? sortingOptions,
        CancellationToken cancellationToken);
    }
}
