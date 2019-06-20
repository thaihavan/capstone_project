using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PostService.Models
{
    public class VirtualTripItem
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("longitude")]
        public float Longitude { get; set; }

        [BsonElement("latitude")]
        public float Latitude { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }

        [BsonElement("image")]
        public string Image { get; set; }

        [BsonElement("location_id")]
        public string LocationId { get; set; }

        [BsonElement("icon")]
        public string Icon { get; set; }

        [BsonElement("formatted_address")]
        public string FormattedAddress { get; set; }
    }
}
