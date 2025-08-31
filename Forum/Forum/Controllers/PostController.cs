using Forum.Application.DTO.Post.Requests;
using Forum.Application.DTO.Post.Responses;
using Forum.Application.Interfaces;
using Forum.DTO.Post.Request;
using Forum.DTO.Post.Response;
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
            var getPostsQuery = new GetPostsQuery
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                Author = query.Author,
                DateFromUtc = query.DateFromUtc,
                DateToUtc = query.DateToUtc,
                Tag = query.Tag,
                SortBy = query.SortBy,
                SortOrder = query.SortOrder
            };
            PaginatedResponse<FullPostResponse> posts = await _postHandler.GetPostsAsync(getPostsQuery, cancellationToken);
            return Ok(posts);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{postId}")]
        public async Task<ActionResult> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            FullPostResponse fullPostResponse = await _postHandler.GetPostByIdAsync(postId, cancellationToken);
            return Ok(fullPostResponse);
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<ActionResult> CreatePostAsync([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
        {
            Guid postId = _postHandler.CreatePostAsync(request.Title, request.Content, UserId, cancellationToken).Result;
            return Ok(new CreatePostResponse { PostId = postId });
        }
    }
}
