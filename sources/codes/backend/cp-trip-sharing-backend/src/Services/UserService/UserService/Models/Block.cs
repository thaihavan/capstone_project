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
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("blocker")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BlockerId { get; set; }

        [BsonElement("blocked")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BlockedId { get; set; }
    }
}
