using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DTOs.Employee
{
    public class GetEmployeesDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
