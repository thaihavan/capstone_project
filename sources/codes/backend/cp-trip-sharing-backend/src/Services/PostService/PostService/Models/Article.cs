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
        public BsonObjectId Id { get; set; }

        [BsonElement("topics")]
        public List<BsonObjectId> Topics { get; set; }

        [BsonElement("destinations")]
        public List<string> Destinations { get; set; }

        [BsonElement("post_id")]
        public BsonObjectId PostId { get; set; }

        [BsonElement("cover_image")]
        public string CoverImage { get; set; }

        [BsonIgnore]
        public Post Post { get; set; }
    }
}
