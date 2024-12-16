using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlyMyKeyBackend.Dtos
{
    public class UserDto
    {
        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string AvatarUrl { get; set; }
    }
}
