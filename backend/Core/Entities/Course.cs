using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using core.Entities;

namespace Core.Entities
{
    public class Course : BaseEntity
    {
        public ObjectId BatchId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }

        [BsonIgnoreIfNull]
        public Batch Batch { get; set; } = null;
    }
}