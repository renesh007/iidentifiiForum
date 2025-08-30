using Forum.Application.Interfaces;
using Forum.DTO.Post.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        public readonly IUserHandler _userHandler;
        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterRequest registerRequest)
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginRequest request, CancellationToken token)
        {
            return Ok();
        }

        [HttpPost("role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRoleAsync([FromBody] UpdateUserRoleRequest request, CancellationToken token)
        {
            return Ok();
        }

    }
}
