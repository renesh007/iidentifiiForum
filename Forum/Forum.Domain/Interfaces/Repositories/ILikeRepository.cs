namespace Forum.Domain.Interfaces.Repository
{
    public interface ILikeRepository
    {

        public Task<int> LikeOrUnlikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken);
    }
}
