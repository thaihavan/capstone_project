using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Like : Model
    {
        [BsonId]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }

        [BsonElement("object_id")]
        public BsonObjectId LikedObject { get; set; }

        [BsonElement("object_type")]
        public BsonString ObjectType { get; set; }

        [BsonElement("date")]
        public BsonDateTime Date { get; set; }

        [BsonElement("user_id")]
        public BsonObjectId UserId { get; set; }

        [BsonIgnore]
        public Post Post { get; set; }
    }
}
