using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.User.UpdateRole
{
    public sealed class UpdateUserRoleRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int UserTypeId { get; set; }
    }
}
