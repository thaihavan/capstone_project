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
        public string Name { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }
    }
}
