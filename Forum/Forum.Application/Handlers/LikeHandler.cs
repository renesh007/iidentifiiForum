using Forum.Application.Exceptions;
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

        ///<inheritdoc/>
        public async Task<string> LikeOrUnlikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken)
        {
            int result = await _likeService.LikeOrUnlikePostAsync(userId, postId, cancellationToken);

            switch (result)
            {
                case 0:
                    return "Post liked.";
                case 1:
                    return "Post Unliked";
                case 1001:
                    throw new PostNotFoundException();
                case 1002:
                    throw new CannotLikeOwnPostException();
                default:
                    throw new InvalidOperationException("Unexpected result from toggle like operation.");
            }
        }
    }
}
