

namespace Forum.Application.Mappers
{
    using Forum.Application.DTO.Post.Requests;
    using Forum.Domain.Models;
    public static class PostQueryMapper
    {
        public static (FilterOptions filter, SortingDirection sortDirection, SortingOptions sortOption)
          ToDomain(this GetPostsQuery query)
        {
            FilterOptions filter = new FilterOptions
            {
                StartDate = query.DateFromUtc,
                EndDate = query.DateToUtc,
                Author = query.Author,
                TagId = query.Tag
            };

            var sortDirection = query.SortOrder?.ToUpper() == "ASCENDING"
                ? SortingDirection.ASCENDING
                : SortingDirection.DECENDING;

            var sortOption = query.SortBy?.ToUpper() switch
            {
                "LIKE" => SortingOptions.LIKE,
                _ => SortingOptions.DATE
            };

            return (filter, sortDirection, sortOption);
        }
    }
}
