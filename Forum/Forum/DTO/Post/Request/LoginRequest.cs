using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Post.Request
{
    public sealed class LoginRequest
    {
        [Required]
        [EmailAddress]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
