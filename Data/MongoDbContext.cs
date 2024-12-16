using MongoDB.Driver;
using OnlyMyKeyBackend.Interfaces;
using OnlyMyKeyBackend.Models;

namespace OnlyMyKeyBackend.Data
{
    public class MongoDbContext: IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        private const string UsersCollection = "users";
        private const string PasswordsCollection = "passwords";

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDb:Name"));
        }

        public IMongoCollection<UserPasswords> Passwords => _database.GetCollection<UserPasswords>(PasswordsCollection);

        public IMongoCollection<User> Users => _database.GetCollection<User>(UsersCollection);
    }
}
