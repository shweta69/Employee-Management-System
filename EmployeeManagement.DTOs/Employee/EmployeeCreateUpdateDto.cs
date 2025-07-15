using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DTOs.Employee
{
    public class EmployeeCreateUpdateDto
    {
        [Required(ErrorMessage = "Employee Name is required")]
        [MinLength(3, ErrorMessage = "Employee Name must be at least 3 characters long")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Username must contain only English letters")]
        public string EmployeeFullName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int DesignationId { get; set; }

        public bool IsActive { get; set; } 
    }
}
