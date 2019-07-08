﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PostService.Models
{
    public class Schedule
    {
        [BsonElement("day")]
        public DateTime Day { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("destination")]
        List<Destination> Destinations { get; set; }
    }
}
