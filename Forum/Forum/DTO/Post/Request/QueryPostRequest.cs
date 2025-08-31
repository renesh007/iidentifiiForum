using System.ComponentModel.DataAnnotations;


namespace Forum.DTO.Post.Request
{
    public class QueryPostRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;

        [MaxLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string? Author { get; set; } = null;

        [DataType(DataType.DateTime)]
        public DateTime? DateFromUtc { get; set; } = null;

        [DataType(DataType.DateTime)]
        public DateTime? DateToUtc { get; set; } = null;

        [MaxLength(36, ErrorMessage = "Tag ID must be a valid string.")]
        public string? Tag { get; set; } = null;

        [AllowedValues("DATE", "LIKE")]
        public string SortBy { get; set; } = "DATE";

        [AllowedValues("ASCENDING", "DESCENDING")]
        public string SortOrder { get; set; } = "DESCENDING";
    }
}
