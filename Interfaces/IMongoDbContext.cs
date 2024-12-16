using MongoDB.Driver;
using OnlyMyKeyBackend.Models;

namespace OnlyMyKeyBackend.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<UserPasswords> Passwords { get; }
    }
}
