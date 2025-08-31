using Forum.Application.Exceptions;
using Forum.Application.Handlers;
using Forum.Domain.Interfaces.Repository;
using NSubstitute;

namespace Forum.Test.Unit.Tests.Application
{
    [TestFixture]
    public class CommentHandlerTests
    {
        private ICommentRepository _commentRepository;
        private CommentHandler _commentHandler;
        private CancellationToken _cancellationToken;

        [SetUp]
        public void SetUp()
        {
            _commentRepository = Substitute.For<ICommentRepository>();
            _commentHandler = new CommentHandler(_commentRepository);
            _cancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task GivenValidResult_WhenCreateCommentAsync_ThenShould_ReturnCommentId()
        {
            // Arrange
            _commentRepository
                .CreateCommentAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(0);

            Guid postId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            // Act
            Guid result = await _commentHandler.CreateCommentAsync(postId, "Test Content", userId, _cancellationToken);

            // Assert
            Assert.AreNotEqual(Guid.Empty, result);
        }

        [Test]
        public void GivenPostNotFound_WhenCreateCommentAsync_ThenShouldThrowPostNotFoundException()
        {
            // Arrange
            _commentRepository
                .CreateCommentAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(1001);

            // Act & Assert
            Assert.ThrowsAsync<PostNotFoundException>(() =>
                _commentHandler.CreateCommentAsync(Guid.NewGuid(), "Test Content", Guid.NewGuid(), _cancellationToken));
        }

        [Test]
        public void GivenUnexpectedCode_WhenCreateCommentAsync_ThenShouldThrowInvalidOperationException()
        {
            // Arrange
            _commentRepository
                .CreateCommentAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(9999);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() =>
                _commentHandler.CreateCommentAsync(Guid.NewGuid(), "Test Content", Guid.NewGuid(), _cancellationToken));
        }
    }
}
