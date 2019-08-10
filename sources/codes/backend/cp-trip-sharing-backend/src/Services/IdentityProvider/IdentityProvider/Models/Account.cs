using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IdentityProvider.Models
{
    public class Account : Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("password_salt")]
        public string PasswordSalt { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("facebook_id")]
        public string FaceBookId { get; set; }

        [BsonElement("google_id")]
        public string GoogleId { get; set; }

        [BsonIgnore]
        public string Token { get; set; }       
    }
}
