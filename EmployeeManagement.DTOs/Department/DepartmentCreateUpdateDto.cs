using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DTOs.Department
{
    public class DepartmentCreateUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }
    }
}
