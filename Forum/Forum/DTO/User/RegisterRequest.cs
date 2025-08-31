using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.User
{
    public sealed class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
