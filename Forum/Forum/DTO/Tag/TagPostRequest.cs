using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Tag
{
    public class TagPostRequest
    {
        [Required]
        public Guid PostId { get; set; }

        [Required]
        public string TagType { get; set; } = string.Empty;
    }
}
