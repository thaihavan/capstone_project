using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Author:Model
    {
        [BsonElement("author_id")]
        public BsonObjectId AuthorId { get; set; }

        [BsonElement("author_name")]
        public string AuthorName { get; set; }

        [BsonElement("author_image")]
        public string AuthorImage { get; set; }
    }
}
