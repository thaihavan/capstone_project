using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    [BsonIgnoreExtraElements]
    public class CompanionPost : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
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

        [BsonElement("estimated_cost_items")]
        public List<string> EstimatedCostItems { get; set; }

        [BsonElement("max_members")]
        public int MaxMembers { get; set; }

        [BsonElement("min_members")]
        public int MinMembers { get; set; }

        [BsonElement("expired_date")]
        public DateTime ExpiredDate { get; set; }

        [BsonElement("schedule")]
        public List<ScheduleItem> ScheduleItems { get; set; }

        [BsonElement("destinations")]
        public List<ArticleDestinationItem> Destinations { get; set; }

        [BsonElement("topics")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Topics { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public Post Post { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public List<CompanionPostJoinRequest> JoinRequests { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public bool Requested { get; set; }
    }
}
