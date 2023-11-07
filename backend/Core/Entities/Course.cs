using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public Batch Batch { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public bool IsActive { get; set; }
    }
}