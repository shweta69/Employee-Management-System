using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Repositories.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<Employee> Employees { get; set; }

        
    }
}
