using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PostService.Models
{
    public class Destination
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("longtitude")]
        public float Longtitude { get; set; }

        [BsonElement("latitude")]
        public string Latitude { get; set; }

    }
}
