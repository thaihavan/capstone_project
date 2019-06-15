using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class VirtualTrip : Model
    {
        [BsonId]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }

        [BsonElement("postId")]
        public BsonObjectId PostId { get; set; }

        [BsonElement("items")]
        public BsonArray Items { get; set; }

        [BsonIgnore]
        public Post Post { get; set; }
    }
}
