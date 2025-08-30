using System.ComponentModel.DataAnnotations;

namespace Forum.DTO.Post.Request
{
    public sealed class UpdateUserRoleRequest
    {
        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required int UserTypeId { get; set; }
    }
}
