namespace Forum.Domain.Interfaces.Repository
{
    public interface ILikeRepository
    {
        /// <summary>
        /// Toggles a user's like on a post.
        /// </summary>
        /// <param name="userId">The ID of the user performing the action.</param>
        /// <param name="postId">The ID of the post to like or unlike.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>
        /// Returns an integer code indicating the outcome of the operation
        /// </returns>
        public Task<int> LikeOrUnlikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken);
    }
}
