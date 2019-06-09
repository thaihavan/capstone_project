using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserServices.Models
{
    public class Photo : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("author")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Author { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
