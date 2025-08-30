using System.Data;

namespace Forum.Infrastructure
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates and returns a new database connection.
        /// </summary>
        /// <remarks>The caller is responsible for opening, using, and disposing of the returned
        /// connection.  Ensure that the connection is properly closed and disposed to avoid resource leaks.</remarks>
        /// <returns>An instance of <see cref="IDbConnection"/> representing the database connection.</returns>
        IDbConnection CreateConnection();
    }
}
