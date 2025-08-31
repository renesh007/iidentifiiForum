using Forum.Application.Exceptions;
using Forum.Application.Handlers;
using Forum.Domain.Interfaces.Repository;
using NSubstitute;

namespace Forum.Test.Unit.Tests.Application
{
    [TestFixture]
    public class TagHandlerTests
    {
        private ITagRepository _tagRepository;
        private TagHandler _tagHandler;
        private CancellationToken _cancellationToken;

        [SetUp]
        public void SetUp()
        {
            _tagRepository = Substitute.For<ITagRepository>();
            _tagHandler = new TagHandler(_tagRepository, Substitute.For<IPostRepository>());
            _cancellationToken = CancellationToken.None;
        }

        [Test]
        public async Task GivenValidResult_WhenTagPostAsync_ThenShouldCompleteWithoutException()
        {
            // Arrange
            _tagRepository
                .TagPostAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(0);

            // Act & Assert
            Assert.DoesNotThrowAsync(() =>
                _tagHandler.TagPostAsync(Guid.NewGuid(), "Tech", Guid.NewGuid(), _cancellationToken));
        }

        [Test]
        public void GivenPostNotFound_WhenTagPostAsync_ThenShouldThrowPostNotFoundException()
        {
            // Arrange
            _tagRepository
                .TagPostAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(1001);

            // Act & Assert
            Assert.ThrowsAsync<PostNotFoundException>(() =>
                _tagHandler.TagPostAsync(Guid.NewGuid(), "Tech", Guid.NewGuid(), _cancellationToken));
        }

        [Test]
        public void GivenTagNotFound_WhenTagPostAsync_ThenShouldThrowTagNotFoundException()
        {
            // Arrange
            _tagRepository
                .TagPostAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(1002);

            // Act & Assert
            Assert.ThrowsAsync<TagNotFoundException>(() =>
                _tagHandler.TagPostAsync(Guid.NewGuid(), "Tech", Guid.NewGuid(), _cancellationToken));
        }

        [Test]
        public void GivenDuplicateTag_WhenTagPostAsync_ThenShouldThrowDuplicateTagException()
        {
            // Arrange
            _tagRepository
                .TagPostAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(1003);

            // Act & Assert
            Assert.ThrowsAsync<DuplicateTagException>(() =>
                _tagHandler.TagPostAsync(Guid.NewGuid(), "Tech", Guid.NewGuid(), _cancellationToken));
        }

        [Test]
        public void GivenUnexpectedCode_WhenTagPostAsync_ThenShouldThrowInvalidOperationException()
        {
            // Arrange
            _tagRepository
                .TagPostAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<Guid>(), _cancellationToken)
                .Returns(9999);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() =>
                _tagHandler.TagPostAsync(Guid.NewGuid(), "Tech", Guid.NewGuid(), _cancellationToken));
        }
    }
}
