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
        public string ObjectId { get; set; }

        [BsonElement("object_type")]
        public string ObjectType { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }
    }
}
