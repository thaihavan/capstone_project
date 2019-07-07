using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Models
{
    public class Notification:Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("receiver")]
        public string Receiver { get; set; }

        [BsonElement("seen")]
        public bool Seen { get; set; }
        
        [BsonElement("date")]
        public DateTime Date { get; set; }



    }
}
