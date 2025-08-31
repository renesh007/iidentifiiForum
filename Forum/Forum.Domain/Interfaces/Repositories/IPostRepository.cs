using Forum.Domain.Entities;

namespace Forum.Domain.Interfaces.Repository
{
    public interface IPostRepository
    {

        public Task<Guid> CreateAsync(string title, string content, Guid userId, CancellationToken cancellationToken);

        public Task<Post?> GetByIdAsync(Guid postId, CancellationToken cancellationToken);

    }
}
