using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DTOs.User
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
         ErrorMessage = "Password must be at least 6 characters and include 1 uppercase, 1 lowercase, 1 digit, and 1 special character")]
        public string Password { get; set; }
    }
}
