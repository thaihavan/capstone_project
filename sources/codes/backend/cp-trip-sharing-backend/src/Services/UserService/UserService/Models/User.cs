using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServices.Models
{
    public class User : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("account_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AccountId { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("display_name")]
        public string DisplayName { get; set; }

        [BsonElement("intersted")]
        public List<string> Instrested { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("dob")]
        public DateTime Dob { get; set; }

        [BsonElement("is_active")]
        public bool Active { get; set; }

        [BsonElement("contribution_point")]
        public int ContributionPoint { get; set; }

        [BsonElement("profile_image")]
        public string Avatar { get; set; }

        [BsonElement("created_date")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("is_first_time")]
        public bool IsFirstTime { get; set; }
    }
}
