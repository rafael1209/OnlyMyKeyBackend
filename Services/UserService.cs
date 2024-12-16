using Google.Apis.Auth;
using MongoDB.Bson;
using OnlyMyKeyBackend.Dtos;
using OnlyMyKeyBackend.Interfaces;
using OnlyMyKeyBackend.Models;

namespace OnlyMyKeyBackend.Services
{
    public class UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<User?> GetOrCreateUserByEmailAsync(GoogleJsonWebSignature.Payload payload)
        {
            var user = await _userRepository.GetByEmailAsync(payload.Email);

            if (user != null)
                return user;

            user = new User
            {
                Id = default,
                Username = payload.Name.Replace(" ", "") + new Random().Next(10000, 19999),
                Email = payload.Email,
                AvatarUrl = payload.Picture,
                AuthToken = _tokenService.GenerateAccessToken(payload.Subject)
            };

            await _userRepository.CreateAsync(user);

            return user;
        }

        public async Task<User?> GetByIdAsync(ObjectId id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await _userRepository.GetByAuthTokenAsync(authToken);
        }

        public async Task<UserDto?> GetDtoByAuthTokenAsync(string authToken)
        {
            var user = await _userRepository.GetByAuthTokenAsync(authToken);

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
        public async Task CreateAsync(User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateAsync(ObjectId id, User user)
        {
            await _userRepository.UpdateAsync(id, user);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
