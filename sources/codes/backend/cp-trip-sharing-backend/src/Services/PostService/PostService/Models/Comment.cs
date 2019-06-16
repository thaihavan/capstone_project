using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Comment : Model
    {
        [BsonId]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }

        [BsonElement("user_id")]
        public BsonObjectId UserId { get; set; }

        [BsonElement("post_id")]
        public BsonObjectId PostId { get; set; }

        [BsonElement("parent_id")]
        public BsonObjectId ParentId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("date")]
        public BsonDateTime Date { get; set; }

        [BsonElement("is_active")]
        public BsonBoolean Active { get; set; }

        [BsonIgnore]
        public Comment Child { get; set; }
    }
}
