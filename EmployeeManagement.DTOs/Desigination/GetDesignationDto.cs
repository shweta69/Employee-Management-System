using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DTOs.Desigination
{
    public class GetDesignationDto
    {
        public int DesignationId { get; set; }
        public string DesignationTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
