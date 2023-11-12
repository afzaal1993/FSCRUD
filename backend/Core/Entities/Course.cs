using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string BatchId { get; set; }
        public Batch Batch { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
    }
}