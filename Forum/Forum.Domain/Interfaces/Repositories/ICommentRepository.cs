namespace Forum.Domain.Interfaces.Repository
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Creates a new comment for the specified post.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to create.</param>
        /// <param name="postId">The unique identifier of the post to which the comment will be added.</param>
        /// <param name="content">The content of the comment. Cannot be null or empty.</param>
        /// <param name="userId">The unique identifier of the user creating the comment.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation will be canceled if the token is triggered.</param>
        /// <returns>
        /// Returns an integer code indicating the outcome of the operation:
        /// <list type="table">
        /// <item>
        ///     <term>0</term>
        ///     <description>Comment was added successfully.</description>
        /// </item>
        /// <item>
        ///     <term>1001</term>
        ///     <description>The post was not found.</description>
        /// </item>
        /// </list>
        /// </returns>
        public Task<int> CreateCommentAsync(Guid commentId, Guid postId, string content, Guid userId, CancellationToken cancellationToken);
    }
}
