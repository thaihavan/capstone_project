using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace ChatService.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string UserId { get; set; }

        [BsonElement("display_name")]
        public string DisplayName { get; set; }

        [BsonElement("connection_id")]
        public List<string> Connections { get; set; }
    }
}
