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
        /// Returns an integer code indicating the outcome of the operation:
        /// <list type="table">
        /// <item>
        ///     <term>0</term>
        ///     <description>Like was added successfully.</description>
        /// </item>
        /// <item>
        ///     <term>1</term>
        ///     <description>Like was removed successfully.</description>
        /// </item>
        /// <item>
        ///     <term>1001</term>
        ///     <description>The post was not found.</description>
        /// </item>
        /// <item>
        ///     <term>1002</term>
        ///     <description>User attempted to like their own post.</description>
        /// </item>
        /// </list>
        /// </returns>
        public Task<int> LikeOrUnlikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken);
    }
}
