using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.User.Login
{
    public sealed class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
