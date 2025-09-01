using Dapper;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repositories;
using Forum.Domain.Models;
using System.Data;

namespace Forum.Infrastructure.Repositories
{
    public class ViewPostRepository : IViewPostRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ViewPostRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<PostView?> GetFullPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string sql = @"
                SELECT *
                FROM vw_PostView
                WHERE PostId = @PostId";

                IEnumerable<FlatPostRow> posts = await connection.QueryAsync<FlatPostRow>(new CommandDefinition(
                 sql,
                 new { PostId = postId },
                 cancellationToken: cancellationToken
             ));

                if (posts == null) return null;

                return MapRowsToPostView(posts);
            }

        }

        public async Task<(IEnumerable<PostView>, int TotalCount)> GetPostsAsync(int pageNumber, int pageSize, FilterOptions? filterOptions, SortingDirection? sortingDirection, SortingOptions? sortingOptions, CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _dbConnectionFactory.CreateConnection())
            {
                var (whereClause, parameters, orderByClause) = BuildSqlFilter(filterOptions, sortingDirection, sortingOptions);

                parameters.Add("Offset", (pageNumber - 1) * pageSize);
                parameters.Add("PageSize", pageSize);

                string sql = $@"
                    SELECT *
                    FROM vw_PostView p
                    {whereClause}
                    ORDER BY {orderByClause}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;";


                string countSql = $@"
                    SELECT COUNT(DISTINCT PostId)
                    FROM vw_PostView p
                    {whereClause};";


                var pagedRows = (await connection.QueryAsync<FlatPostRow>(
                    new CommandDefinition(sql, parameters, cancellationToken: cancellationToken)
                )).ToList();

                int totalCount = await connection.ExecuteScalarAsync<int>(
                    new CommandDefinition(countSql, parameters, cancellationToken: cancellationToken)
                );


                var posts = pagedRows
                    .GroupBy(r => r.PostId)
                    .Select(g => MapRowsToPostView(g.ToList()))
                    .ToList();

                return (posts, totalCount);
            }
        }

        private static string BuildWhereClause(FilterOptions? filterOptions, out DynamicParameters parameters)
        {
            var whereClauses = new List<string>();
            parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(filterOptions?.Author))
            {
                whereClauses.Add("u.Name = @Author");
                parameters.Add("Author", filterOptions.Author);
            }

            if (filterOptions?.StartDate != null)
            {
                whereClauses.Add("p.CreatedOn >= @StartDate");
                parameters.Add("StartDate", filterOptions.StartDate);
            }

            if (filterOptions?.EndDate != null)
            {
                whereClauses.Add("p.CreatedOn <= @EndDate");
                parameters.Add("EndDate", filterOptions.EndDate);
            }

            if (!string.IsNullOrEmpty(filterOptions?.TagId))
            {
                whereClauses.Add("t.Name = @Tag");
                parameters.Add("Tag", filterOptions.TagId);
            }

            return whereClauses.Any() ? "WHERE " + string.Join(" AND ", whereClauses) : "";
        }

        private string BuildOrderByClause(SortingOptions? sortingOptions, SortingDirection? sortingDirection)
        {
            string orderByColumn = sortingOptions?.ToString() switch
            {
                "Title" => "p.Title",
                _ => "p.CreatedOn"
            };

            string direction = sortingDirection?.ToString() == "ASCENDING" ? "ASC" : "DESC";
            return $"{orderByColumn} {direction}";
        }

        private (string whereClause, DynamicParameters parameters, string orderByClause) BuildSqlFilter(
            FilterOptions? filterOptions,
            SortingDirection? sortingDirection,
            SortingOptions? sortingOptions)
        {
            string whereClause = BuildWhereClause(filterOptions, out DynamicParameters parameters);
            string orderByClause = BuildOrderByClause(sortingOptions, sortingDirection);
            return (whereClause, parameters, orderByClause);
        }
        private PostView MapRowsToPostView(IEnumerable<FlatPostRow> rows)
        {
            var firstRow = rows.First();

            var post = new PostView
            {
                PostId = firstRow.PostId,
                Title = firstRow.Title,
                Content = firstRow.Content,
                CreatedOn = firstRow.CreatedOn,
                Author = firstRow.Author,
                TotalLikes = firstRow.TotalLikes,
                Comments = rows
                    .Where(r => r.CommentId.HasValue)
                    .Select(r => new CommentView
                    {
                        CommentId = r.CommentId!.Value,
                        CommentContent = r.CommentContent!,
                        CommentCreatedOn = r.CommentCreatedOn!.Value,
                        CommentAuthor = r.CommentAuthor!
                    })
                    .ToList(),
                Tags = rows
                    .Where(r => !string.IsNullOrEmpty(r.TagName))
                    .Select(r => r.TagName!)
                    .Distinct()
                    .ToList()
            };

            return post;
        }

        private class FlatPostRow
        {
            public Guid PostId { get; set; }
            public string Title { get; set; } = null!;
            public string Content { get; set; } = null!;
            public DateTime CreatedOn { get; set; }
            public string Author { get; set; } = null!;
            public int TotalLikes { get; set; }

            public Guid? CommentId { get; set; }
            public string? CommentContent { get; set; }
            public DateTime? CommentCreatedOn { get; set; }
            public string? CommentAuthor { get; set; }

            public string? TagName { get; set; }
        }
    }
}
