namespace Forum.Application.Interfaces
{
    public interface ITagHandler
    {
        /// <summary>
        /// Associates a specified tag with a post on behalf of a user.
        /// </summary>
        /// <param name="postId">The unique identifier of the post to be tagged.</param>
        /// <param name="tagType">The name or type of the tag to apply to the post.</param>
        /// <param name="userId">The unique identifier of the user performing the tagging.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous operation. Completes successfully if the tag is applied. 
        /// </returns>
        public Task TagPostAsync(Guid postId, string tagType, Guid userId, CancellationToken cancellationToken);
    }
}
