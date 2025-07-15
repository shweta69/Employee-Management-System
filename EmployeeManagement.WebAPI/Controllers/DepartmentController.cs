using EmployeeManagement.DTOs.Department;
using EmployeeManagement.Repositories.DB_Context;
using EmployeeManagement.Repositories.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class DepartmentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    private readonly ILogger<DesignationController> _logger;

    public DepartmentController(ApplicationDbContext context, ILogger<DesignationController> logger )
    {
        _context = context;
        _logger = logger;
    }

    //Create Department
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try 
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                IsActive = dto.IsActive
            };

            _context.departments.Add(department);
            await _context.SaveChangesAsync();

            return Ok("Department created successfully");
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database error while creating Department.");
            return BadRequest("Could not create Department. Please try again.");
        }

    }

    //Get All Departments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDepartmentDto>>> GetAll()
    {
        var departments = await _context.departments
            .Select(d => new GetDepartmentDto
            {
                DepartmentId = d.DepartmentId,
                DepartmentName = d.DepartmentName,
                IsActive = d.IsActive
            })
            .ToListAsync();

        return Ok(departments);
    }

    //Get Department by Id
    [HttpGet("{id}")]
    public async Task<ActionResult<GetDepartmentDto>> GetById(int id)
    {
        var department = await _context.departments.FindAsync(id);
        if (department == null) return NotFound();

        var dto = new GetDepartmentDto
        {
            DepartmentId = department.DepartmentId,
            DepartmentName = department.DepartmentName,
            IsActive = department.IsActive
        };

        return Ok(dto);
    }

    //Update Department
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DepartmentCreateUpdateDto dto)
    {
        try
        {
            var department = await _context.departments.FindAsync(id);
            if (department == null) return NotFound();

            department.DepartmentName = dto.DepartmentName;
            department.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return Ok("Department updated successfully");
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database error while updating Department.");
            return BadRequest("Could not update Department. Please try again.");
        }

    }

    //Delete (Deactivate)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.departments.FindAsync(id);
        if (department == null) return NotFound();

        if (!department.IsActive)
            return BadRequest("Department already inactive");

        department.IsActive = false;
        await _context.SaveChangesAsync();

        return Ok("Department deactivated successfully");
    }
}
