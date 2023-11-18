namespace core.DTOs
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public IFormFile formFile { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdateStudentDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public IFormFile formFile { get; set; } = null;
        public string ModifiedBy { get; set; }
    }

    public class GetStudentDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
