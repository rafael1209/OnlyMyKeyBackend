using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlyMyKeyBackend.Models
{
    public class PasswordEntry
    {
        public string? Login { get; set; }

        public string? EncryptedPassword { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
