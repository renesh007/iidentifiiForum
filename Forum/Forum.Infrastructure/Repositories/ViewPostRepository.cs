using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repositories;
using Forum.Domain.Models;

namespace Forum.Infrastructure.Repositories
{
    internal class ViewPostRepository : IViewPostRepository
    {
        public async Task<PostView?> GetFullPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<PostView>, int TotalCount)> GetPostsAsync(int pageNumber, int pageSize, FilterOptions? filterOptions, SortingDirection? sortingDirection, SortingOptions? sortingOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
