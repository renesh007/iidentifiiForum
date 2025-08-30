namespace Forum.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(Guid userId, string userType);
    }
}
