using Forum.Domain.Interfaces.Services;

namespace Forum.Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        public string GenerateToken(Guid userId, string userType)
        {
            throw new NotImplementedException();
        }
    }
}
