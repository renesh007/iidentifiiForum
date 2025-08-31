using Dapper;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repository;
using System.Data;

namespace Forum.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public PostRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Guid> CreateAsync(Guid postId, string title, string content, Guid userId, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                        INSERT INTO dbo.tb_Post (Id, Title, Content, UserId)
                        VALUES (@PostID, @Title, @Content, @UserID);";

                await connection.ExecuteAsync(
                       new CommandDefinition(
                           sql,
                           new { PostID = postId, Title = title, Content = content, UserID = userId },
                           cancellationToken: cancellationToken
                       )
   );
                return postId;
            }
        }

        public async Task<Post?> GetByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                        SELECT Id, Title, Content, UserID, CreatedOn
                        FROM dbo.tb_Post
                        WHERE Id = @PostID;";
                var post = await connection.QueryFirstOrDefaultAsync<Post>(
                       new CommandDefinition(
                           sql,
                           new { PostID = postId },
                           cancellationToken: cancellationToken
                           )
                       );

                return post;
            }
        }
    }
}