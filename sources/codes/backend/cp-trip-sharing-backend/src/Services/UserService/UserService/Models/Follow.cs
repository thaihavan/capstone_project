using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserServices.Models
{
    public class Follow : Model
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("follower")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Follower { get; set; }

        [BsonElement("following")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Following { get; set; }
    }
}
