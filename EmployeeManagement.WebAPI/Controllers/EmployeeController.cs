using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Repositories.DB_Context;
using EmployeeManagement.Repositories.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    //Create Employee
    [Authorize(Roles = "Admin,HR")]
    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var employee = new Employee
        {
            EmployeeFullName = dto.EmployeeFullName,
            Email = dto.Email,
            DepartmentId = dto.DepartmentId,
            DesignationId = dto.DesignationId,
            IsActive = dto.IsActive
        };

        _context.employees.Add(employee);
        await _context.SaveChangesAsync();

        return Ok("Employee created successfully");
    }

    //Get All Employees
    [Authorize(Roles = "Admin,HR")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetEmployeesDto>>> GetAll()
    {
        var employees = await _context.employees
            .Include(e => e.Department)
            .Include(e => e.Designation)
            .Select(e => new GetEmployeesDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeFullName = e.EmployeeFullName,
                Email = e.Email,
                DepartmentName = e.Department.DepartmentName,
                DesignationTitle = e.Designation.DesignationTitle,
                IsActive = e.IsActive
            })
            .ToListAsync();

        return Ok(employees);
    }

    //Get By ID
    [Authorize(Roles = "Admin,HR,Employee")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _context.employees
            .Include(e => e.Department)
            .Include(e => e.Designation)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);

        if (employee == null) return NotFound();

        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (role == "Employee" && employee.Email != email)
            return Forbid("Employees can only view their own profile");

        var dto = new GetEmployeesDto
        {
            EmployeeId = employee.EmployeeId,
            EmployeeFullName = employee.EmployeeFullName,
            Email = employee.Email,
            DepartmentName = employee.Department.DepartmentName,
            DesignationTitle = employee.Designation.DesignationTitle,
            IsActive = employee.IsActive
        };

        return Ok(dto);
    }

    //Update
    [Authorize(Roles = "Admin,HR")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeCreateUpdateDto dto)
    {
        var employee = await _context.employees.FindAsync(id);
        if (employee == null) return NotFound();

        employee.EmployeeFullName = dto.EmployeeFullName;
        employee.Email = dto.Email;
        employee.DepartmentId = dto.DepartmentId;
        employee.DesignationId = dto.DesignationId;
        employee.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return Ok("Employee updated successfully");
    }

    //delete: Only Admins can
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.employees.FindAsync(id);
        if (employee == null) return NotFound();

        if (!employee.IsActive)
            return BadRequest("Employee is already deactivated.");

        employee.IsActive = false;
        await _context.SaveChangesAsync();

        return Ok("Employee deactivated successfully");
    }
}
