using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Article : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("topics")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Topics { get; set; }

        [BsonElement("destinations")]
        public List<string> Destinations { get; set; }

        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("cover_image")]
        public string CoverImage { get; set; }

        [BsonIgnore]
        public Post Post { get; set; }

        [BsonIgnore]
        public bool liked { get; set; }
    }
}
