using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Models
{
    public class MessageDetail : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("from_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FromUserId { get; set; }

        [BsonElement("conversation_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConversationId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("time")]
        public DateTime Time { get; set; }
    }
}
