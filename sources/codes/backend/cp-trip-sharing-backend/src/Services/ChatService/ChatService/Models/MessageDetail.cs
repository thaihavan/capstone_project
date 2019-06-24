using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Models
{
    public class MessageDetail
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string FromUserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ToUserId { get; set; }

        public string Content { get; set; }
    }
}
