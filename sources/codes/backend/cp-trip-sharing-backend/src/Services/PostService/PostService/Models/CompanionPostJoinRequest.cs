using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    [BsonIgnoreExtraElements]
    public class CompanionPostJoinRequest : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("companion_post_id")]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string CompanionPostId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public Author User { get; set; }

        
    }
}
