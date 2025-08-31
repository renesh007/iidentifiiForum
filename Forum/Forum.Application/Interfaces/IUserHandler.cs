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
        /// <returns>A string containing authentication token.</returns>
        public Task<string> LoginUserAsync(string email, string password, CancellationToken ct);

        /// <summary>
        /// Updates the role of a user to the specified user type.
        /// </summary>
        /// <remarks>This method updates the user's role based on the provided user type identifier.  Ensure that the
        /// <paramref name="newUserTypeId"/> corresponds to a valid user type in the system.</remarks>
        /// <param name="userId">The unique identifier of the user whose role is to be updated.</param>
        /// <param name="newUserTypeId">The identifier of the new user type to assign to the user.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task UpdateUserRoleAsync(Guid userId, int newUserTypeId, CancellationToken ct);
    }
}
