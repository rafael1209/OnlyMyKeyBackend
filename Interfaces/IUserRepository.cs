using MongoDB.Bson;
using OnlyMyKeyBackend.Models;

namespace OnlyMyKeyBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(ObjectId id);
        Task<User?> GetByAuthTokenAsync(string authToken);
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(ObjectId id, User user);
        Task DeleteAsync(ObjectId id);
        Task CreateAsync(User user);
    }
}
