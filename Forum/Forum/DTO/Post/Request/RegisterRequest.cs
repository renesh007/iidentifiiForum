using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Post.Request
{
    public sealed class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
