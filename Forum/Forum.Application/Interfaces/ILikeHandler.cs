namespace Forum.Application.Interfaces
{
    public interface ILikeHandler
    {
        /// <summary>
        /// Adds a like to a post for the specified user.  
        /// Validates business rules before performing the operation:
        /// - The post must exist.
        /// - The user cannot like their own post.
        /// - The user cannot like the same post more than once.
        /// </summary>
        /// <param name="userId">The ID of the user who is liking the post.</param>
        /// <param name="postId">The ID of the post to be liked.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>
        /// A task representing the asynchronous operation.  
        /// Throws the following exceptions on failure:
        /// <list type="bullet">
        /// <item><description><see cref="PostNotFoundException"/> if the specified post does not exist.</description></item>
        /// <item><description><see cref="CannotLikeOwnPostException"/> if the user attempts to like their own post.</description></item>
        /// <item><description><see cref="DuplicateLikeException"/> if the user has already liked the post (optional, depending on implementation).</description></item>
        /// </list>
        /// </returns>
        public Task LikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken);
    }
}
