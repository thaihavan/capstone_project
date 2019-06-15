using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PostService.Models
{
    public class VirtualTripItem : Model
    {
        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("longtitude")]
        public double longtitude { get; set; }

        [BsonElement("latitude")]
        public double latitude { get; set; }

        [BsonElement("note")]
        public string note { get; set; }

        [BsonElement("image")]
        public string image { get; set; }
    }
}
