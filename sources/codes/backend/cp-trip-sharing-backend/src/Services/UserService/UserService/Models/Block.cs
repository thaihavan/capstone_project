using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServices.Models
{
    public class Block : Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("blocker")]
        public string BlockerId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("blocked")]
        public string BlockedId { get; set; }
    }
}
