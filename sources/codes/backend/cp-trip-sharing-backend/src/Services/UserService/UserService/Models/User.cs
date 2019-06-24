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
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("account_id")]
        public string AccountId { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("display_name")]
        public string DisplayName { get; set; }

        [BsonElement("interested")]
        public List<string> Interested { get; set; }

        [BsonElement("gender")]
        public bool Gender { get; set; }

        [BsonElement("dob")]
        public DateTime? Dob { get; set; }

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

        [BsonElement("address")]
        public string Address { get; set; }
    }
}
