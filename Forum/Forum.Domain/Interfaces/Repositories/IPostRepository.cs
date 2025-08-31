using Forum.Domain.Entities;

namespace Forum.Domain.Interfaces.Repository
{
    public interface IPostRepository
    {
        /// <summary>
        /// Creates a new entity with the specified title, content, and associated user ID.
        /// </summary>
        /// <param name="title">The title of the entity to create. Cannot be null or empty.</param>
        /// <param name="content">The content of the entity to create. Cannot be null or empty.</param>
        /// <param name="userId">The unique identifier of the user associated with the entity.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Guid"/> representing the unique identifier of the newly created entity.</returns>
        public Task<Guid> CreateAsync(string title, string content, Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a post by its unique identifier.
        /// </summary>
        /// <param name="postId">The unique identifier of the post to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation will be canceled if the token is triggered.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Post"/> if found;
        /// otherwise, <see langword="null"/>.</returns>
        public Task<Post?> GetByIdAsync(Guid postId, CancellationToken cancellationToken);

    }
}
