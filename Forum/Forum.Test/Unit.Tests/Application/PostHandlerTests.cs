using Forum.Application.DTO.Post.Responses;
using Forum.Application.Exceptions;
using Forum.Application.Handlers;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repositories;
using Forum.Domain.Interfaces.Repository;
using Forum.Domain.Models;
using NSubstitute;

namespace Forum.Test.Unit.Tests.Application
{
    [TestFixture]
    public class PostHandlerTests
    {
        private IPostRepository _postRepository;
        private IViewPostRepository _viewPostRepository;
        private PostHandler _handler;
        private CancellationToken _ct;

        [SetUp]
        public void SetUp()
        {
            _postRepository = Substitute.For<IPostRepository>();
            _viewPostRepository = Substitute.For<IViewPostRepository>();
            _handler = new PostHandler(_postRepository, _viewPostRepository);
            _ct = CancellationToken.None;
        }

        [Test]
        public async Task GivenValidPostData_WhenCreatePostAsyncIsCalled_ThenShouldReturnPostId()
        {
            // Arrange
            Guid expectedPostId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            _postRepository.CreateAsync(Arg.Any<Guid>(), "title", "content", userId, _ct)
                .Returns(Task.FromResult(expectedPostId));

            // Act
            Guid result = await _handler.CreatePostAsync("title", "content", userId, _ct);

            // Assert
            Assert.AreEqual(expectedPostId, result);
            await _postRepository.Received(1).CreateAsync(Arg.Any<Guid>(), "title", "content", userId, _ct);
        }

        [Test]
        public async Task GivenPostExists_WhenGetPostByIdAsyncIsCalled_ThenShouldReturnFullPostResponse()
        {
            // Arrange
            Guid postId = Guid.NewGuid();
            PostView postView = new PostView
            {
                PostId = postId,
                Title = "My Post",
                Content = "Content",
                CreatedAt = DateTime.UtcNow,
                Author = "Author",
                TotalLikes = 5,
                Comments = new List<Comment>
                {
                    new Comment { Id = Guid.NewGuid(), Content = "Comment 1", CreatedOn = DateTime.UtcNow, CreatedBy = "Commenter" }
                },
                Tags = new List<string> { "tag1", "tag2" }
            };

            _viewPostRepository.GetFullPostByIdAsync(postId, _ct).Returns(Task.FromResult(postView));

            // Act
            FullPostResponse result = await _handler.GetPostByIdAsync(postId, _ct);

            // Assert
            Assert.AreEqual(postId, result.PostId);
            Assert.AreEqual("My Post", result.Title);
            Assert.AreEqual("Content", result.Content);
            Assert.AreEqual(5, result.TotalLikes);
            Assert.AreEqual(1, result.Comments.Count);
            Assert.AreEqual(2, result.Tags.Count);
        }

        [Test]
        public void GivenPostDoesNotExist_WhenGetPostByIdAsyncIsCalled_ThenShouldThrowException()
        {
            // Arrange
            Guid postId = Guid.NewGuid();
            _viewPostRepository.GetFullPostByIdAsync(postId, _ct).Returns(Task.FromResult<PostView?>(null));

            // Act & Assert
            Assert.ThrowsAsync<PostNotFoundException>(async () => await _handler.GetPostByIdAsync(postId, _ct));
        }

        [Test]
        public async Task GivenPostsExist_WhenGetPostsAsyncIsCalled_ThenShouldReturnPaginatedResponse()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 2;
            int totalCount = 5;

            List<PostView> postViews = new List<PostView>
            {
                new PostView
                {
                    PostId = Guid.NewGuid(),
                    Title = "Post 1",
                    Content = "Content 1",
                    CreatedAt = DateTime.UtcNow,
                    Author = "Author1",
                    TotalLikes = 2,
                    Comments = new List<Comment>(),
                    Tags = new List<string>{"tag1"}
                },
                new PostView
                {
                    PostId = Guid.NewGuid(),
                    Title = "Post 2",
                    Content = "Content 2",
                    CreatedAt = DateTime.UtcNow,
                    Author = "Author2",
                    TotalLikes = 3,
                    Comments = new List<Comment>(),
                    Tags = new List<string>{"tag2"}
                }
            };

            _viewPostRepository
                .GetPostsAsync(pageNumber, pageSize, Arg.Any<FilterOptions?>(), Arg.Any<SortingDirection?>(), Arg.Any<SortingOptions?>(), _ct)
                .Returns(Task.FromResult((postViews.AsEnumerable(), totalCount)));

            // Act
            var result = await _handler.GetPostsAsync(_ct, null, null, null, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(totalCount, result.TotalItems);
            Assert.AreEqual(pageNumber, result.PageNumber);
            Assert.AreEqual(3, result.TotalPages);
            Assert.AreEqual("Post 1", result.Items.First().Title);
            Assert.AreEqual("Post 2", result.Items.Last().Title);
        }
    }
}

