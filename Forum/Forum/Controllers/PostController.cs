using Forum.Application.Interfaces;
using Forum.DTO.Post.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseApiController
    {
        private readonly IPostHandler _postHandler;

        public PostController(IPostHandler postHandler)
        {
            _postHandler = postHandler;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("posts")]
        public async Task<ActionResult> GetPostsAsync([FromQuery] QueryPostRequest query, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{postId}")]
        public async Task<ActionResult> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<ActionResult> CreatePostAsync([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
