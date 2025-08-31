namespace Forum.Application.Interfaces
{
    public interface ICommentHandler
    {
        /// <summary>
        /// Creates a new comment for the specified post.
        /// </summary>
        /// <param name="postId">The unique identifier of the post to which the comment will be added.</param>
        /// <param name="content">The content of the comment. Cannot be null or empty.</param>
        /// <param name="userId">The unique identifier of the user creating the comment.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the operation. If the operation is canceled, the task will be marked as
        /// canceled.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the unique identifier of the
        /// newly created comment.</returns>
        public Task<Guid> CreateCommentAsync(Guid postId, string content, Guid userId, CancellationToken cancellationToken);
    }
}
