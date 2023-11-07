using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class CourseDto
    {
        public int BatchId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetCourseDto
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string BatchName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
    }
}