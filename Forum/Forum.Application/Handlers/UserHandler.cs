using Forum.Application.DTO;
using Forum.Application.Exceptions;
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
        public async Task<LoginResponse> LoginUserAsync(string email, string password, CancellationToken ct)
        {
            User user = await _userRepository.GetUserByEmailAsync(email, ct);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            PasswordVerificationResult passwordVerificationResult = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

            string token = _tokenService.GenerateToken(user.Id, user.UserType);

            return new LoginResponse
            {
                Token = token,
            };
        }

        ///<inheritdoc/>
        public async Task<Guid> RegisterUserAsync(string name, string email, string password, CancellationToken ct)
        {
            User user = await _userRepository.GetUserByEmailOrName(email, name, ct);

            if (user != null)
            {
                if (user.Email == email)
                    throw new EmailAlreadyExistsException();
                if (user.Name == name)
                    throw new UsernameAlreadyExistsException();
            }

            User newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                UserType = "User"
            };

            newUser.PasswordHash = _hasher.HashPassword(newUser, password);

            Guid userId = await _userRepository.RegisterUserAsync(newUser, ct);

            return userId;
        }

        ///<inheritdoc/>
        public async Task UpdateUserRoleAsync(Guid userId, int newUserTypeId, CancellationToken ct)
        {
            User? user = await _userRepository.GetUserByIdAsync(userId, ct);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            await _userRepository.UpdateUserRoleAsync(userId, newUserTypeId, ct);
        }
    }
}
