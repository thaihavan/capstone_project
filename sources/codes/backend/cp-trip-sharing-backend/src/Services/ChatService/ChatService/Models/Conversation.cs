using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Models
{
    public class Conversation : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("avatar")]
        public string Avatar { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("receiver_list")]
        public List<string> Receivers { get; set; }

        [BsonElement("created_date")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("group_admin")]
        public string GroupAdmin { get; set; }

        [BsonElement("seen_ids")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SeenIds { get; set; }

        [BsonIgnore]
        public MessageDetail LastMessage { get; set; }

        [BsonIgnore]
        public List<MessageDetail> Messages { get; set; }

        [BsonIgnore]
        public List<User> Users { get; set; }

    }
}
