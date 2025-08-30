using Forum.Application.DTO;
using Forum.Application.Interfaces;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces.Repository;
using Forum.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _hasher;
        public UserHandler(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _hasher = passwordHasher;
        }

        ///<inheritdoc/>    
        public Task<LoginResponse> LoginUserAsync(string email, string password, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Task<Guid> RegisterUserAsync(string name, string email, string password, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public Task UpdateUserRoleAsync(Guid userId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
