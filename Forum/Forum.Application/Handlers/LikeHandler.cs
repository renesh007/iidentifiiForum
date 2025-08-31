using Forum.Application.Interfaces;
using Forum.Domain.Interfaces.Repository;

namespace Forum.Application.Handlers
{
    public class LikeHandler : ILikeHandler
    {
        private readonly ILikeRepository _likeService;
        public LikeHandler(ILikeRepository likeService, IPostRepository postRepository)
        {
            _likeService = likeService;
        }

        public async Task LikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
