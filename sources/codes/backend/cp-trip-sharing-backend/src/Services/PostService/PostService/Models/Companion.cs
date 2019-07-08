using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Companion : Model
    {
        [BsonId]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("post_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("coversation_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ConversationId { get; set; }

        [BsonElement("from")]
        public DateTime From { get; set; }

        [BsonElement("to")]
        public DateTime To { get; set; }

        [BsonElement("estimated_cost")]
        public double EstimatedCost { get; set; }

        [BsonElement("schedule")]
        List<Schedule> Schedules { get; set; }
    }
}
