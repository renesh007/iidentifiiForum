namespace Forum.Application.Interfaces
{
    public interface ILikeHandler
    {

        public Task LikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken);
    }
}
