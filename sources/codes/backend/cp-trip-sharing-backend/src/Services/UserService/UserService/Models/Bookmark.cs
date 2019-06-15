using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserServices.Models
{
    public class Bookmark : Model
    {
        [BsonId]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }
  
        [BsonElement("user_id")]
        public BsonObjectId UserId { get; set; }

        [BsonElement("post_id")]
        public BsonObjectId PostId { get; set; }

    }
}
