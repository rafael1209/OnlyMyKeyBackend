using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlyMyKeyBackend.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("avatarUrl")]
        public required string AvatarUrl { get; set; }

        [BsonElement("accessToken")]
        public required string AuthToken { get; set; }
    }
}
