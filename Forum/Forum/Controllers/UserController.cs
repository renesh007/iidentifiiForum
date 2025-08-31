using Forum.Application.Interfaces;
using Forum.DTO.User.Login;
using Forum.DTO.User.Register;
using Forum.DTO.User.UpdateRole;
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
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegisterRequest registerRequest, CancellationToken ct)
        {
            Guid userId = await _userHandler.RegisterUserAsync(registerRequest.Name, registerRequest.Email, registerRequest.Password, ct);

            return Ok(new RegisterResponse { UserId = userId });
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginRequest request, CancellationToken ct)
        {
            string response = await _userHandler.LoginUserAsync(request.Email, request.Password, ct);

            return Ok(new LoginResponse { Token = response });
        }

        [HttpPatch("role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUserRoleAsync([FromBody] UpdateUserToModeratorRequest request, CancellationToken ct)
        {
            await _userHandler.UpdateUserToModeratorAsync(request.UserId, ct);

            return NoContent();
        }

    }
}
