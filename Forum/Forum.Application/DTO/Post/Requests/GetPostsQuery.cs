namespace Forum.Application.DTO.Post.Requests
{
    public class GetPostsQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Author { get; set; }
        public DateTime? DateFromUtc { get; set; }
        public DateTime? DateToUtc { get; set; }
        public string? Tag { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
