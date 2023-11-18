using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;

namespace Core.Entities
{
    public class StudentCourse : BaseEntity
    {
        public ObjectId StudentId { get; set; }
        public ObjectId CourseId { get; set; }
        public string EnrollDate { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }

        [BsonIgnoreIfNull]
        public Student Student { get; set; } = null;

        [BsonIgnoreIfNull]
        public Course Course { get; set; } = null;
    }
}