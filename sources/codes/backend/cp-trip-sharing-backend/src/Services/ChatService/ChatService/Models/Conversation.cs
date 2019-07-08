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
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Type { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("receiver_list")]
        public List<string> Receivers { get; set; }

        [BsonElement("last_message")]
        public string LastMessage { get; set; }

        [BsonElement("group_admin")]
        public string GroupAdmin { get; set; }

        [BsonIgnore]
        public List<MessageDetail> Messages { get; set; }

        [BsonIgnore]
        public List<User> Users { get; set; }

    }
}
