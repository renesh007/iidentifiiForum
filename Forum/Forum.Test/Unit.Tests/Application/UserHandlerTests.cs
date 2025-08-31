using Forum.Application.Exceptions;
using Forum.Application.Handlers;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repository;
using Forum.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace Forum.Test.Unit.Tests.Application
{
    [TestFixture]
    public class UserHandlerTests
    {
        private IUserRepository _userRepository;
        private ITokenService _tokenService;
        private IPasswordHasher<User> _passwordHasher;
        private UserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _tokenService = Substitute.For<ITokenService>();
            _passwordHasher = Substitute.For<IPasswordHasher<User>>();

            _handler = new UserHandler(_userRepository, _tokenService, _passwordHasher);
        }

        [Test]
        public async Task GivenValidCredentials_WhenLoginUserAsync_ThenReturnsToken()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";

            User user = new User
            {
                Id = Guid.NewGuid(),
                PasswordHash = "hashedPassword",
                Email = email,
                UserType = "Admin"
            };

            _userRepository.GetUserByEmailAsync(email, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(user));

            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password)
                           .Returns(PasswordVerificationResult.Success);

            _tokenService.GenerateToken(user.Id, user.UserType).Returns("token123");

            // Act
            var result = await _handler.LoginUserAsync(email, password, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("token123", result);
        }

        [Test]
        public void GivenNonexistentUser_WhenLoginUserAsync_ThenThrowsException()
        {
            // Arrange
            _userRepository.GetUserByEmailAsync("test@example.com", Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult<User>(null));

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(() =>
                _handler.LoginUserAsync("test@example.com", "password", CancellationToken.None));
        }

        [Test]
        public void GivenInvalidPassword_WhenLoginUserAsync_ThenThrowsUnauthorizedAccessException()
        {
            string email = "test@example.com";
            string password = "wrongpassword";

            // Arrange
            User user = new User
            {
                Id = Guid.NewGuid(),
                PasswordHash = "hashedPassword",
                UserType = "Admin"
            };

            _userRepository.GetUserByEmailAsync(email, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(user));

            _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password)
                           .Returns(PasswordVerificationResult.Failed);

            // Act & Assert
            Assert.ThrowsAsync<InvalidCredentialsException>(() =>
                _handler.LoginUserAsync(email, password, CancellationToken.None));
        }

        [Test]
        public async Task GivenNewUser_WhenRegisterUserAsync_ThenReturnsUserId()
        {
            // Arrange
            _userRepository.GetUserByEmailOrName("new@example.com", "New User", Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult<User>(null));

            _passwordHasher.HashPassword(Arg.Any<User>(), Arg.Any<string>())
                           .Returns("hashedPassword");

            _userRepository.RegisterUserAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
                          .Returns(ci => ((User)ci[0]).Id);

            // Act
            Guid userId = await _handler.RegisterUserAsync("New User", "new@example.com", "password", CancellationToken.None);

            // Assert
            Assert.AreNotEqual(Guid.Empty, userId);
            await _userRepository.Received(1).RegisterUserAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public void GivenExistingUserEmail_WhenRegisterUserAsync_ThenThrowsEmailAlreadyExistsException()
        {
            // Arrange
            User existingUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "existing@example.com",
                Name = "OtherName" // Different to test email conflict
            };

            _userRepository.GetUserByEmailOrName(existingUser.Email, Arg.Any<string>(), Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(existingUser));

            // Act & Assert
            Assert.ThrowsAsync<EmailAlreadyExistsException>(async () =>
                await _handler.RegisterUserAsync("NewUserName", existingUser.Email, "password", CancellationToken.None));
        }

        [Test]
        public void GivenExistingUserName_WhenRegisterUserAsync_ThenThrowsUsernameAlreadyExistsException()
        {
            // Arrange
            User existingUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "other@example.com",
                Name = "existingUsername" // Conflict on username
            };

            _userRepository.GetUserByEmailOrName(Arg.Any<string>(), existingUser.Name, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(existingUser));

            // Act & Assert
            Assert.ThrowsAsync<UsernameAlreadyExistsException>(async () =>
                await _handler.RegisterUserAsync(existingUser.Name, "new@example.com", "password", CancellationToken.None));
        }

        [Test]
        public async Task GivenExistingUser_WhenUpdateUserRoleAsync_ThenCallsRepository()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            User user = new User { Id = userId };
            int ModratorRoleId = 2;

            _userRepository.GetUserByIdAsync(userId, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult(user));

            // Act
            await _handler.UpdateUserToModeratorAsync(userId, CancellationToken.None);

            // Assert
            await _userRepository.Received(1).UpdateUserRoleAsync(userId, ModratorRoleId, Arg.Any<CancellationToken>());
        }

        [Test]
        public void GivenNonexistentUser_WhenUpdateUserRoleAsync_ThenThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            _userRepository.GetUserByIdAsync(userId, Arg.Any<CancellationToken>())
                           .Returns(Task.FromResult<User>(null));

            // Act & Assert
            Assert.ThrowsAsync<UserNotFoundException>(() =>
                _handler.UpdateUserToModeratorAsync(userId, CancellationToken.None));
        }

    }
}
