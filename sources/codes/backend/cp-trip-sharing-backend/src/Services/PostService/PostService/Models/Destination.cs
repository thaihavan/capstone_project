using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    
    public class Destination:Model
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("longitude")]
        public float Longitude { get; set; }

        [BsonElement("latitude")]
        public float Latitude { get; set; }

    }
}
