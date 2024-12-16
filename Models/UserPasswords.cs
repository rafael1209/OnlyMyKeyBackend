using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlyMyKeyBackend.Models
{
    public class UserPasswords
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonId]
        public ObjectId UserId { get; set; }

        public List<PasswordEntry>? Passwords { get; set; }
    }
}
