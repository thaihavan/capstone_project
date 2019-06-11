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
        public ObjectId Id { get; set; }

        [BsonElement("blocker")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId BlockerId { get; set; }

        [BsonElement("blocked")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId BlockedId { get; set; }
    }
}
