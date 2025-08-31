using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Like
{
    public class LikeRequest
    {
        [Required]
        public Guid PostId { get; set; }
    }
}
