using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class Email:Model
    {
        [BsonId]
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("to")]
        public string To { get; set; }

        [BsonElement("token")]
        public string Token { get; set; }

        [BsonElement("email_type")]
        public string EmailType { get; set; }

        [BsonElement("date")]
        public BsonDateTime Date { get; set; }
    }
}
