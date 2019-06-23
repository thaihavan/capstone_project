using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Comment : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("author_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }

        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("parent_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("is_active")]
        public bool Active { get; set; }

        [BsonElement("like_count")]
        public int LikeCount { get; set; }

        [BsonIgnore]
        public List<Comment> Childs { get; set; }

        [BsonIgnore]
        public Author Author { get; set; }

        [BsonIgnore]
        public bool Liked { get; set; }

        public Comment()
        {
            Childs = new List<Comment>();
        }
    }
}
