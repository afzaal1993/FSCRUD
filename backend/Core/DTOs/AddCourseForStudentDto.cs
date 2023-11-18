using Core.Entities;

namespace core.DTOs
{
    public class AddCourseForStudentDto
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public string EnrollDate { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
    }

    public class GetStudentCourseDto
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string EnrollDate { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
    }
}
