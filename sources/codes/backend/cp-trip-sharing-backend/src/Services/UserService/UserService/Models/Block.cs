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
        public BsonObjectId Id { get; set; }

        [BsonElement("blocker")]
        public BsonObjectId BlockerId { get; set; }

        [BsonElement("blocked")]
        public BsonObjectId BlockedId { get; set; }
    }
}
