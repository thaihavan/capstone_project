using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Post : Model
    {
        [BsonId]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("is_public")]
        public bool IsPublic { get; set; }

        [BsonElement("is_active")]
        public bool IsActive { get; set; }

        [BsonElement("pub_date")]
        public DateTime PubDate { get; set; }

        [BsonElement("post_type")]
        public string PostType { get; set; }

        [BsonElement("author")]
        public Author Author { get; set; }

        [BsonElement("like_count")]
        public int LikeCount { get; set; }

        [BsonElement("comment_count")]
        public int CommentCount { get; set; }

    }
}
