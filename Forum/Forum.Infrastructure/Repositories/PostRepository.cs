using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repository;

namespace Forum.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public PostRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Guid> CreateAsync(string title, string content, Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Post?> GetByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
