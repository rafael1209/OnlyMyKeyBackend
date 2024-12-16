using MongoDB.Bson;
using MongoDB.Driver;
using OnlyMyKeyBackend.Data;
using OnlyMyKeyBackend.Interfaces;
using OnlyMyKeyBackend.Models;

namespace OnlyMyKeyBackend.Repositories
{
    public class UserRepository(MongoDbContext context) : IUserRepository
    {
        private readonly IMongoCollection<User> _users = context.Users;

        public async Task<User> GetByIdAsync(ObjectId id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await _users.Find(user => user.AuthToken == authToken).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ObjectId id, User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == id, user);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
