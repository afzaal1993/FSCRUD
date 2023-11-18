using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CourseDto
    {
        public string BatchId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateCourseDto
    {
        public string BatchId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class GetCourseDto
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string BatchName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}