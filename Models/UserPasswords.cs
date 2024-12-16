using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlyMyKeyBackend.Models
{
    public class UserPasswords
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("userId")]
        public ObjectId UserId { get; set; }
        [BsonElement("passwords")]
        public List<PasswordEntry>? Passwords { get; set; }
    }
}
