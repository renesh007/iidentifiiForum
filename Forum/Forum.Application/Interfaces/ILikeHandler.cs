namespace Forum.Application.Interfaces
{
    public interface ILikeHandler
    {
        /// <summary>
        /// Adds or Removes a like to a post for the specified user.  
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
        /// </returns>
        public Task<string> LikeOrUnlikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken);
    }
}
