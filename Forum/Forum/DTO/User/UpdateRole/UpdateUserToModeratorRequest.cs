using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.User.UpdateRole
{
    public class UpdateUserToModeratorRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
