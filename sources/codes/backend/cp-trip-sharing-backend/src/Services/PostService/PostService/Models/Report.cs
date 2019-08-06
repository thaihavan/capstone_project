using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    [BsonIgnoreExtraElements]
    public class Report:Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("report_type")]
        public string ReportType { get; set; }

        [BsonElement("object_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }

        [BsonElement("object_type")]
        public string ObjectType { get; set; }

        [BsonElement("reporter_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReporterId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
