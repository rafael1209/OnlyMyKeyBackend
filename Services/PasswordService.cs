using MongoDB.Bson;
using OnlyMyKeyBackend.Interfaces;
using OnlyMyKeyBackend.Models;
using OnlyMyKeyBackend.Repositories;
using OnlyMyKeyBackend.Requests;

namespace OnlyMyKeyBackend.Services
{
    public class PasswordService(IPasswordRepository passwordRepository)
    {
        private readonly IPasswordRepository _passwordRepository = passwordRepository;

        public async Task<List<PasswordEntry>> GetByUserIdAsync(ObjectId userId)
        {
            return await _passwordRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateAsync(ObjectId id, PasswordEntry password)
        {
            await _passwordRepository.UpdateAsync(id, password);
        }

        public async Task DeleteByIndexAsync(ObjectId userId, int index)
        {
            await _passwordRepository.DeleteByIndexAsync(userId, index);
        }

        public async Task AddNewPasswordToList(ObjectId userId, CreatePassword request)
        {
            await _passwordRepository.AddNewPasswordToList(userId, request);
        }
    }
}
