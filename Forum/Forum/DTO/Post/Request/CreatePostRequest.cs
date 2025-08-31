using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Post.Request
{
    public class CreatePostRequest
    {
        [Required]
        [StringLength(100)]
        public required string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(4000)]
        public required string Content { get; set; } = string.Empty;
    }
}
