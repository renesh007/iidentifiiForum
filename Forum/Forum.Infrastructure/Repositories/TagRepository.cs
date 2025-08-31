using Dapper;
using Forum.Domain.Interfaces.Repository;
using System.Data;

namespace Forum.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public TagRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<int> TagPostAsync(Guid postTagId, Guid postId, string tagType, Guid userId, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"
                    DECLARE @Result INT;
                    DECLARE @TagId INT;

                    -- Check if post exists
                    IF NOT EXISTS (SELECT 1 FROM tb_Post WHERE Id = @PostId)
                    BEGIN
                        SET @Result = 1001;
                    END
                    -- Check if tag type exists
                    ELSE IF NOT EXISTS (SELECT 1 FROM tb_Tag WHERE Name = @TagType)
                    BEGIN
                        SET @Result = 1002; 
                    END
                    -- Check for duplicate
                    ELSE IF EXISTS (SELECT 1 FROM tb_PostTag WHERE PostId = @PostId AND TagId = (SELECT Id FROM tb_Tag WHERE Name = @TagType) AND UserId = @UserId)
                    BEGIN
                        SET @Result = 1003; 
                    END
                    ELSE
                    BEGIN
                        SELECT @TagId = Id FROM tb_Tag WHERE Name = @TagType;

                        INSERT INTO tb_PostTag (Id, PostId, UserId, TagId)
                        VALUES (@PostTagId, @PostId, @UserId, @TagId);

                        SET @Result = 0; 
                    END

                    SELECT @Result;";

                return await connection.ExecuteScalarAsync<int>(new CommandDefinition(
                    sql,
                    new { PostTagId = postTagId, PostId = postId, TagType = tagType, UserId = userId },
                    cancellationToken: cancellationToken
                ));
            }
        }
    }
}
