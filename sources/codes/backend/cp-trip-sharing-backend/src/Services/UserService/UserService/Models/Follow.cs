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
        public BsonObjectId Id { get; set; }

        [BsonElement("follower")]
        public BsonObjectId Follower { get; set; }

        [BsonElement("following")]
        public BsonObjectId Following { get; set; }
    }
}
