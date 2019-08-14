using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    [BsonIgnoreExtraElements]
    public class Bookmark : Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("post_id")]
        public string PostId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("author_id")]
        public string AuthorId { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public Author Author { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public Post Post { get; set; }
    }
}

