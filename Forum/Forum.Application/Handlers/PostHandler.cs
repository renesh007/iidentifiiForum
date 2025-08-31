using Forum.Application.DTO.Comment;
using Forum.Application.DTO.Post.Requests;
using Forum.Application.DTO.Post.Responses;
using Forum.Application.Exceptions;
using Forum.Application.Interfaces;
using Forum.Application.Mappers;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repositories;
using Forum.Domain.Interfaces.Repository;

namespace Forum.Application.Handlers
{
    public class PostHandler : IPostHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly IViewPostRepository _viewPostRepository;

        public PostHandler(IPostRepository postRepository, IViewPostRepository viewPostRepository)
        {
            _postRepository = postRepository;
            _viewPostRepository = viewPostRepository;
        }

        public async Task<Guid> CreatePostAsync(string title, string content, Guid userId, CancellationToken cancellationToken)
        {
            Guid postId = Guid.NewGuid();
            return await _postRepository.CreateAsync(postId, title, content, userId, cancellationToken);
        }

        public async Task<FullPostResponse> GetPostByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            PostView? enrichedPost = await _viewPostRepository.GetFullPostByIdAsync(postId, cancellationToken);

            if (enrichedPost == null)
            {
                throw new PostNotFoundException();
            }

            return new FullPostResponse
            {
                PostId = enrichedPost.PostId,
                Title = enrichedPost.Title,
                Content = enrichedPost.Content,
                CreatedAt = enrichedPost.CreatedAt,
                Author = enrichedPost.Author,
                TotalLikes = enrichedPost.TotalLikes,
                Comments = enrichedPost.Comments.Select(c => new CommentResponse
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    CreatedBy = c.CreatedBy
                }).ToList(),
                Tags = enrichedPost.Tags
            };
        }

        public async Task<PaginatedResponse<FullPostResponse>> GetPostsAsync(GetPostsQuery getPostsQuery, CancellationToken cancellationToken)
        {
            var (filterOptions, sortingDirection, sortingOptions) = getPostsQuery.ToDomain();

            (IEnumerable<PostView> posts, int totalCount) = await _viewPostRepository.GetPostsAsync(getPostsQuery.PageNumber, getPostsQuery.PageSize, filterOptions, sortingDirection, sortingOptions, cancellationToken);

            var response = posts.Select(p => new FullPostResponse
            {
                PostId = p.PostId,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Author = p.Author,
                TotalLikes = p.TotalLikes,
                Comments = p.Comments.Select(c => new CommentResponse
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    CreatedBy = c.CreatedBy
                }).ToList(),
                Tags = p.Tags
            }).ToList();

            return new PaginatedResponse<FullPostResponse>
            {
                Items = response,
                TotalItems = totalCount,
                PageNumber = getPostsQuery.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)getPostsQuery.PageSize)
            };

        }
    }
}
