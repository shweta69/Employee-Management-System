using EmployeeManagement.DTOs.Desigination;
using EmployeeManagement.Repositories.DB_Context;
using EmployeeManagement.Repositories.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class DesignationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    private readonly ILogger<DesignationController> _logger;

    public DesignationController(ApplicationDbContext context, ILogger<DesignationController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //Create
    [HttpPost]
    public async Task<IActionResult> Create(DesignationCreateUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try {
            var designation = new Designation
            {
                DesignationTitle = dto.DesignationTitle,
                IsActive = dto.IsActive
            };

            _context.Designations.Add(designation);
            await _context.SaveChangesAsync();

            return Ok("Designation created successfully");
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database error while creating designation.");
            return BadRequest("Could not create designation. Please try again.");
        }

    }

    //Get All records
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDesignationDto>>> GetAll()
    {
        var designations = await _context.Designations
            .Select(d => new GetDesignationDto
            {
                DesignationId = d.DesignationId,
                DesignationTitle = d.DesignationTitle,
                IsActive = d.IsActive
            })
            .ToListAsync();

        return Ok(designations);
    }

    //Get record By Id
    [HttpGet("{id}")]
    public async Task<ActionResult<GetDesignationDto>> GetById(int id)
    {
        var designation = await _context.Designations.FindAsync(id);
        if (designation == null) return NotFound();

        var dto = new GetDesignationDto
        {
            DesignationId = designation.DesignationId,
            DesignationTitle = designation.DesignationTitle,
            IsActive = designation.IsActive
        };

        return Ok(dto);
    }

    //Update record
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DesignationCreateUpdateDto dto)
    {
        try
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation == null) return NotFound();

            designation.DesignationTitle = dto.DesignationTitle;
            designation.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return Ok("Designation updated successfully");
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Database error while updating designation.");
            return BadRequest("Could not update designation. Please try again.");
        }

    }

    //Deactivate record
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var designation = await _context.Designations.FindAsync(id);
        if (designation == null) return NotFound();

        if (!designation.IsActive)
            return BadRequest("Designation already inactive");

        designation.IsActive = false;
        await _context.SaveChangesAsync();

        return Ok("Designation deactivated successfully");
    }
}
