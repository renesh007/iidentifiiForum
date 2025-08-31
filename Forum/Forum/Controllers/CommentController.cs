using Forum.Application.Interfaces;
using Forum.DTO.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseApiController
    {
        private readonly ICommentHandler _commentHandler;

        public CommentController(ICommentHandler commentHandler)
        {
            _commentHandler = commentHandler;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddCommentAsync([FromBody] CommentRequest commentRequest, CancellationToken cancellationToken)
        {
            Guid response = await _commentHandler.CreateCommentAsync(commentRequest.PostId, commentRequest.Content, UserId, cancellationToken);

            return Ok(new CommentResponse { CommentId = response });
        }
    }
}
