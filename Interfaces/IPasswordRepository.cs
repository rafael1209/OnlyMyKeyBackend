using MongoDB.Bson;
using OnlyMyKeyBackend.Models;
using OnlyMyKeyBackend.Requests;

namespace OnlyMyKeyBackend.Interfaces
{
    public interface IPasswordRepository
    {
        Task<List<PasswordEntry>> GetByUserIdAsync(ObjectId userId);
        Task UpdateAsync(ObjectId id, PasswordEntry password);
        Task DeleteByIndexAsync(ObjectId userId, int id);
        Task CreateAsync();
        Task AddNewPasswordToList(ObjectId userId, CreatePassword request);
    }
}
