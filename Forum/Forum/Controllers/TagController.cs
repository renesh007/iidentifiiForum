using Forum.Application.Interfaces;
using Forum.DTO.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : BaseApiController
    {
        private readonly ITagHandler _tagHandler;
        public TagController(ITagHandler tagHandler)
        {
            _tagHandler = tagHandler;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult> TagPostAsync([FromBody] TagPostRequest request, CancellationToken cancellationToken)
        {
            Guid response = await _tagHandler.TagPostAsync(request.PostId, request.TagType, UserId, cancellationToken);
            return Ok(new TagPostResponse { PostTagId = response });
        }
    }
}
