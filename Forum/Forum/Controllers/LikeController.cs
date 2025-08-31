using Forum.Application.Interfaces;
using Forum.DTO.Like;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : BaseApiController
    {
        public readonly ILikeHandler _likeHandler;

        public LikeController(ILikeHandler likeHandler)
        {
            _likeHandler = likeHandler;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> LikePost([FromBody] LikeRequest likeRequest, CancellationToken cancellationToken)
        {
            await _likeHandler.LikePostAsync(UserId, likeRequest.PostId, cancellationToken);
            return Ok();
        }
    }
}
