using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Repositories.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeFullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Foreign Keys
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }

        public int? UserId { get; set; }

        // Navigation Properties
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public User User { get; set; }


    }
}
