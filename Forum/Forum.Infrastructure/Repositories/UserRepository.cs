using Dapper;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repository;
using System.Data;

namespace Forum.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken ct)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                    SELECT 
                        u.Id, 
                        u.Name, 
                        u.Email,
                        u.PasswordHash,
                        ut.Description AS UserType
                    FROM dbo.tb_User u
                    JOIN dbo.tb_UserType ut ON u.UserTypeID = ut.Id
                    WHERE u.Email = @Email";

                User? user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition(sql, new { Email = email }, cancellationToken: ct));

                return user;
            }
        }


        public async Task<User> GetUserByEmailOrName(string email, string name, CancellationToken ct)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                    SELECT 
                        u.Id, 
                        u.Name, 
                        u.Email, 
                        ut.Description AS UserType
                    FROM dbo.tb_User u
                    JOIN dbo.tb_UserType ut ON u.UserTypeID = ut.Id
                    WHERE u.Email = @Email
                    OR u.Name = @Name";

                User? user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition(sql, new { Email = email, Name = name }, cancellationToken: ct));

                return user;
            }
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken ct)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                    SELECT 
                        u.Id, 
                        u.Name, 
                        u.Email,
                        u.PasswordHash,
                        ut.Description AS UserType
                    FROM dbo.tb_User u
                    JOIN dbo.tb_UserType ut ON u.UserTypeID = ut.Id
                    WHERE u.Id = @UserId";

                User? user = await connection.QueryFirstOrDefaultAsync<User>(new CommandDefinition(sql, new { UserId = userId }, cancellationToken: ct));

                return user;
            }
        }

        public async Task<Guid> RegisterUserAsync(User user, CancellationToken ct)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                    INSERT INTO dbo.tb_User (Id, Name, Email, PasswordHash, UserTypeId)
                    VALUES (@Id, @Name, @Email, @PasswordHash, 
                    (SELECT Id FROM dbo.tb_UserType WHERE Description = @UserType));";

                await connection.ExecuteAsync(
                    new CommandDefinition(sql, new
                    {
                        user.Id,
                        user.Name,
                        user.Email,
                        user.PasswordHash,
                        user.UserType
                    }, cancellationToken: ct)
                );

                return user.Id;
            }
        }

        public async Task UpdateUserRoleAsync(Guid userId, int newUserTypeId, CancellationToken ct)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                    UPDATE dbo.tb_User
                    SET UserTypeId = @NewUserTypeId
                    WHERE Id = @Id;";

                await connection.ExecuteAsync(
                    new CommandDefinition(sql, new { Id = userId, NewUserTypeId = newUserTypeId }, cancellationToken: ct)
                );
            }
        }
    }
}
