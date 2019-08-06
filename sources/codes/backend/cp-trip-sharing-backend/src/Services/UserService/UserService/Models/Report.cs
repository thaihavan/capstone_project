using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserServices.Models
{
    [BsonIgnoreExtraElements]
    public class Report:Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("report_type_id")]
        public string ReportTypeId { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public User User { get; set; }

        [BsonIgnore]
        [BsonExtraElements]
        public ReportType ReportType { get; set; }
    }
}
