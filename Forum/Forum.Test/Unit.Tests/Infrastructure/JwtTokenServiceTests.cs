using Forum.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Forum.Test.Unit.Tests.Infrastructure
{
    [TestFixture]
    public class JwtTokenServiceTests
    {
        private JwtTokenService _jwtTokenService;
        private IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            var jwtSettings = new Dictionary<string, string>
        {
            { "Jwt:Key", "super_secret_key_12345678900000000000" },
            { "Jwt:Issuer", "TestIssuer" },
            { "Jwt:Audience", "TestAudience" },
            { "Jwt:ExpireMinutes", "60" }
        };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(jwtSettings)
                .Build();

            _jwtTokenService = new JwtTokenService(_configuration);
        }

        [Test]
        public void GivenValidUser_WhenGenerateTokenCalled_ThenShouldContainCorrectClaims()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var role = "Admin";

            // Act
            var token = _jwtTokenService.GenerateToken(userId, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            Assert.That(userIdClaim, Is.EqualTo(userId.ToString()));
            Assert.That(roleClaim, Is.EqualTo(role));
        }

        [Test]
        public void GivenValidUser_WhenGenerateTokenCalled_ThenShouldSetCorrectIssuerAndAudience()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var role = "User";

            // Act
            var token = _jwtTokenService.GenerateToken(userId, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.That(jwtToken.Issuer, Is.EqualTo("TestIssuer"));
            Assert.That(jwtToken.Audiences.Contains("TestAudience"), Is.True);
        }

        [Test]
        public void GivenValidUser_WhenGenerateTokenCalled_ThenShouldHaveValidExpiration()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var role = "User";
            var expectedExpiry = DateTime.UtcNow.AddMinutes(60);

            // Act
            var token = _jwtTokenService.GenerateToken(userId, role);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            Assert.That(jwtToken.ValidTo, Is.EqualTo(expectedExpiry).Within(TimeSpan.FromSeconds(5)));
        }
    }
}
