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
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("items")]
        public List<VirtualTripItem> Items { get; set; }

        [BsonIgnore]
        public Post Post { get; set; }
    }
}
