using MongoDB.Bson;
using MongoDB.Driver;
using OnlyMyKeyBackend.Data;
using OnlyMyKeyBackend.Interfaces;
using OnlyMyKeyBackend.Models;
using OnlyMyKeyBackend.Requests;

namespace OnlyMyKeyBackend.Repositories
{
    public class PasswordRepository(MongoDbContext context) : IPasswordRepository
    {
        private readonly IMongoCollection<UserPasswords> _passwords = context.Passwords;

        public async Task<List<PasswordEntry>> GetByUserIdAsync(ObjectId userId)
        {
            var userPasswords = await _passwords.Find(p => p.UserId == userId).FirstOrDefaultAsync();

            return userPasswords?.Passwords ?? [];
        }


        public Task UpdateAsync(ObjectId id, PasswordEntry password)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIndexAsync(ObjectId userId, int index)
        {
            var passwords = await GetByUserIdAsync(userId);

            passwords.RemoveAt(index);

            var filter = Builders<UserPasswords>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<UserPasswords>.Update.Set(u => u.Passwords, passwords);

            await _passwords.UpdateOneAsync(filter, update);
        }


        public async Task AddNewPasswordToList(ObjectId userId, CreatePassword request)
        {
            var newPasswordEntry = new PasswordEntry
            {
                Login = request.Login,
                EncryptedPassword = request.Password,
                Description = request.Note,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            var filter = Builders<UserPasswords>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<UserPasswords>.Update.Push(u => u.Passwords, newPasswordEntry);

            await _passwords.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }
}
