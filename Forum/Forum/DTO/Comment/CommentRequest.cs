using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Comment
{
    public class CommentRequest
    {
        [Required]
        public Guid PostId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;
    }
}
