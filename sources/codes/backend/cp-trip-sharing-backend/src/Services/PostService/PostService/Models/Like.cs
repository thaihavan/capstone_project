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
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("object_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }

        [BsonElement("object_type")]
        public string ObjectType { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
