using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace IAM.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords donot match")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; } = true;

        [Required]
        public int RoleId { get; set; }
    }
}
