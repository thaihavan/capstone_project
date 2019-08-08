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
        [BsonElement("report_type_id")]
        public string ReportTypeId { get; set; }

        [BsonElement("reported_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("reported_type")]
        public string PostType { get; set; }

        [BsonElement("reporter_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReporterId { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public Article Article { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public CompanionPost CompanionPost { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public VirtualTrip VirtualTrip { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public Comment Comment { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public ReportType ReportType { get; set; }                
    }
}
