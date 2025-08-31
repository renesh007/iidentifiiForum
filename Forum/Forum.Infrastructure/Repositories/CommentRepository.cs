using Dapper;
using Forum.Domain.Interfaces.Repository;
using System.Data;

namespace Forum.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public CommentRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> CreateCommentAsync(Guid commentId, Guid postId, string content, Guid userId, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"
                    DECLARE @Result INT;

                    -- Check post exists
                    IF NOT EXISTS (SELECT 1 FROM tb_Post WHERE Id = @PostId)
                    BEGIN
                        SET @Result = 1001;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO tb_Comment (Id, PostId, Content, UserId, CreatedOn)
                        VALUES (@CommentId, @PostId, @Content, @UserId, SYSUTCDATETIME());
            
                        SET @Result = 0;
                    END

                    SELECT @Result;";

                return await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                    sql,
                    new { CommentId = commentId, PostId = postId, Content = content, UserId = userId },
                    cancellationToken: cancellationToken
                ));
            }
        }
    }
}
