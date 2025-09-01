namespace Forum.Domain.Interfaces.Repository
{
    public interface ITagRepository
    {
        /// <summary>
        /// Asynchronously tags a post with the specified tag type on behalf of a user.
        /// </summary>
        /// <remarks>This method associates a tag with a post, allowing the post to be categorized or
        /// identified by the specified tag type. The user performing the operation is identified by <paramref
        /// name="userId"/>.</remarks>
        /// <param name="postTagId">The unique identifier of the post tag to create.</param>
        /// <param name="postId">The unique identifier of the post to be tagged.</param>
        /// <param name="tagType">The type of tag to apply to the post. This value cannot be null or empty.</param>
        /// <param name="userId">The unique identifier of the user performing the tagging operation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation will be canceled if the token is triggered.</param>
        /// <returns>A task that represents the asynchronous operation and the result contains the unique identifier of the
        /// newly created tag.</returns>
        public Task<int> TagPostAsync(Guid postTagId, Guid postId, string tagType, Guid userId, CancellationToken cancellationToken);
    }
}
