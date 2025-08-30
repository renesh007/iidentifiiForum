namespace Forum.Domain.Interfaces.Services
{
    /// <summary>
    /// Provides functionality for generating authentication tokens for users.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a token for the specified user and user type.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="userType">The type or role of the user.</param>
        /// <returns>A string representing the generated authentication token.</returns>
        public string GenerateToken(Guid userId, string userType);
    }
}
