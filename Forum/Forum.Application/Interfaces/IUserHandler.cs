using Forum.Application.DTO;

namespace Forum.Application.Interfaces
{
    /// <summary>
    /// Defines user-related operations such as registration, authentication, and role management.
    /// </summary>
    public interface IUserHandler
    {
        /// <summary>
        /// Registers a new user with the specified name, email, and password.
        /// </summary>
        /// <param name="name">The user's display name.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="ct">Cancellation token for the operation.</param>
        /// <returns>The unique identifier of the newly registered user.</returns>
        public Task<Guid> RegisterUserAsync(string name, string email, string password, CancellationToken ct);

        /// <summary>
        /// Authenticates a user using their email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="ct">Cancellation token for the operation.</param>
        /// <returns>A <see cref="LoginResponse"/> containing authentication details.</returns>
        public Task<LoginResponse> LoginUserAsync(string email, string password, CancellationToken ct);

        /// <summary>
        /// Updates the role of the specified user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="ct">Cancellation token for the operation.</param>
        public Task UpdateUserRoleAsync(Guid userId, CancellationToken ct);
    }
}
