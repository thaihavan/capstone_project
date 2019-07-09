using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Models
{
    public class Notification : Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("receivers")]
        public List<string> Receivers { get; set; }

        [BsonElement("seen")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Seen { get; set; }
        
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("display_image")]
        public string DisplayImage { get; set; }
    }
}
