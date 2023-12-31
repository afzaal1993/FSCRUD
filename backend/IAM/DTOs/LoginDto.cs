﻿using System.ComponentModel.DataAnnotations;

namespace IAM.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
