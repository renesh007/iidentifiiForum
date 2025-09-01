using System.ComponentModel.DataAnnotations;


namespace Forum.DTO.Post.Request
{
    public class QueryPostRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
        public int? PageNumber { get; set; }

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int? PageSize { get; set; }

        [MaxLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string? Author { get; set; } = null;

        [DataType(DataType.DateTime)]
        public DateTime? DateFromUtc { get; set; } = null;

        [DataType(DataType.DateTime)]
        public DateTime? DateToUtc { get; set; } = null;

        [MaxLength(36, ErrorMessage = "Tag ID must be a valid string.")]
        public string? Tag { get; set; } = null;

        public string? SortBy { get; set; } = "DATE";

        public string? SortOrder { get; set; } = "DESCENDING";
    }
}
