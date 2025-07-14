using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Repositories.Entities
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }

        [Required]
        public string DesignationTitle { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<Employee> Employees { get; set; }
    }
}
