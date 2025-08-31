using Forum.Application.Exceptions;
using Forum.Application.Handlers;
using Forum.Domain.Interfaces.Repository;
using NSubstitute;

namespace Forum.Test.Unit.Tests.Application
{
    [TestFixture]
    public class LikeHandlerTests
    {
        private ILikeRepository _likeRepository;
        private LikeHandler _handler;
        private Guid _userId;
        private Guid _postId;
        private CancellationToken _ct;

        [SetUp]
        public void SetUp()
        {
            _likeRepository = Substitute.For<ILikeRepository>();
            _handler = new LikeHandler(_likeRepository, null!); // postRepository is not used in your handler
            _userId = Guid.NewGuid();
            _postId = Guid.NewGuid();
            _ct = CancellationToken.None;
        }

        [Test]
        public void GivenRepositoryReturns0_WhenLikePostAsyncIsCalled_ThenDoesNotThrow()
        {
            // Arrange
            _likeRepository.LikeOrUnlikePostAsync(_userId, _postId, _ct).Returns(0);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _handler.LikePostAsync(_userId, _postId, _ct));
        }

        [Test]
        public Task GivenRepositoryReturns1_WhenLikePostAsyncIsCalled_ThenDoesNotThrow()
        {
            // Arrange
            _likeRepository.LikeOrUnlikePostAsync(_userId, _postId, _ct).Returns(1);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _handler.LikePostAsync(_userId, _postId, _ct));
            return Task.CompletedTask;
        }

        [Test]
        public void GivenRepositoryReturns1001_WhenLikePostAsyncIsCalled_ThenThrowsPostNotFoundException()
        {
            // Arrange
            _likeRepository.LikeOrUnlikePostAsync(_userId, _postId, _ct).Returns(1001);

            // Act & Assert
            Assert.ThrowsAsync<PostNotFoundException>(async () => await _handler.LikePostAsync(_userId, _postId, _ct));
        }

        [Test]
        public void GivenRepositoryReturns1002_WhenLikePostAsyncIsCalled_ThenThrowsCannotLikeOwnPostException()
        {
            // Arrange
            _likeRepository.LikeOrUnlikePostAsync(_userId, _postId, _ct).Returns(1002);

            // Act & Assert
            Assert.ThrowsAsync<CannotLikeOwnPostException>(async () => await _handler.LikePostAsync(_userId, _postId, _ct));
        }

        [Test]
        public void GivenRepositoryReturnsUnexpectedValue_WhenLikePostAsyncIsCalled_ThenThrowsInvalidOperationException()
        {
            // Arrange
            _likeRepository.LikeOrUnlikePostAsync(_userId, _postId, _ct).Returns(9999);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.LikePostAsync(_userId, _postId, _ct));
        }

        [Test]
        public async Task GivenValidLike_WhenLikePostAsyncIsCalled_ThenRepositoryCalledExactlyOnce()
        {
            // Arrange
            _likeRepository.LikeOrUnlikePostAsync(_userId, _postId, _ct).Returns(0);

            // Act
            await _handler.LikePostAsync(_userId, _postId, _ct);

            // Assert
            await _likeRepository.Received(1).LikeOrUnlikePostAsync(_userId, _postId, _ct);
        }
    }

}
