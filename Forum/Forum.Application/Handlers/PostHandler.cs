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
            PostView? postView = await _viewPostRepository.GetFullPostByIdAsync(postId, cancellationToken);

            if (postView == null)
            {
                throw new PostNotFoundException();
            }

            return new FullPostResponse
            {
                PostId = postView.PostId,
                Title = postView.Title,
                Content = postView.Content,
                CreatedOn = postView.CreatedOn,
                Author = postView.Author,
                TotalLikes = postView.TotalLikes,
                Comments = postView.Comments.Select(c => new CommentResponse
                {
                    Id = c.CommentId,
                    Content = c.CommentContent,
                    CreatedOn = c.CommentCreatedOn,
                    CreatedBy = c.CommentAuthor
                }).ToList(),
                Tags = postView.Tags
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
                CreatedOn = p.CreatedOn,
                Author = p.Author,
                TotalLikes = p.TotalLikes,
                Comments = p.Comments.Select(c => new CommentResponse
                {
                    Id = c.CommentId,
                    Content = c.CommentContent,
                    CreatedOn = c.CommentCreatedOn,
                    CreatedBy = c.CommentAuthor
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
