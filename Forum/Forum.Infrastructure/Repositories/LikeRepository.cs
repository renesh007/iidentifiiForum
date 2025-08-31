using Dapper;
using Forum.Domain.Interfaces.Repository;
using System.Data;

namespace Forum.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public LikeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> LikeOrUnlikePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"
                    DECLARE @Result INT;

                    IF NOT EXISTS (SELECT 1 FROM tb_Post WHERE Id = @PostId)
                    BEGIN
                        SET @Result = 1001;
                    END
                    ELSE IF EXISTS (SELECT 1 FROM tb_Like l INNER JOIN tb_Post p ON p.Id = l.PostId WHERE l.UserId = @UserId AND l.PostId = @PostId)
                    BEGIN
                        DELETE l
                        FROM tb_Like l
                        WHERE l.UserId = @UserId AND l.PostId = @PostId;
                        SET @Result = 1;
                    END
                    ELSE IF EXISTS (SELECT 1 FROM tb_Post WHERE Id = @PostId AND UserId = @UserId)
                    BEGIN
                        SET @Result = 1002;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO tb_Like (UserId, PostId) VALUES (@UserId, @PostId);
                        SET @Result = 0;
                    END

                    SELECT @Result;";


                return await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                         sql,
                         new { UserId = userId, PostId = postId },
                         cancellationToken: cancellationToken
                     ));
            }
        }
    }
}
