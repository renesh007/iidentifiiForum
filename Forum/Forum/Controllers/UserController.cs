using Forum.Application.DTO;
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
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterRequest registerRequest, CancellationToken ct)
        {
            Guid userId = await _userHandler.RegisterUserAsync(registerRequest.Name, registerRequest.Email, registerRequest.Password, ct);

            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginRequest request, CancellationToken ct)
        {
            LoginResponse response = await _userHandler.LoginUserAsync(request.Email, request.Password, ct);

            return Ok(response);
        }

        [HttpPost("role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRoleAsync([FromBody] UpdateUserRoleRequest request, CancellationToken ct)
        {
            await _userHandler.UpdateUserRoleAsync(request.UserId, request.UserTypeId, ct);

            return Ok();
        }

    }
}
