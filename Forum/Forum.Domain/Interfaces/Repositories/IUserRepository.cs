using Forum.Domain.Entities;

namespace Forum.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Registers a new user with the specified name, email, and password hash.
        /// </summary>
        /// <param name="name">The name of the user to register. Cannot be null or empty.</param>
        /// <param name="email">The email address of the user to register. Must be a valid email format and cannot be null or empty.</param>
        /// <param name="passwordHash">The hashed password of the user. Cannot be null or empty.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Guid"/> representing the unique identifier of the newly registered user.</returns>
        Task<Guid> RegisterUserAsync(string name, string email, string passwordHash, CancellationToken ct);

        /// <summary>
        /// Asynchronously retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve. Cannot be null or empty.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="User"/>
        /// associated with the specified email address, or <see langword="null"/> if no user is found.</returns>
        Task<User> GetUserByEmailAsync(string email, CancellationToken ct);

        /// <summary>
        /// Updates the role of a user identified by the specified user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose role is to be updated.</param>
        /// <param name="ct">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateUserRoleAsync(Guid userId, CancellationToken ct);
    }
}
