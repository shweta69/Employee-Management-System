using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DTOs.Desigination
{
    public class DesignationCreateUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string DesignationTitle { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
