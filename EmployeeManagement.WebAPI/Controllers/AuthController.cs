using EmployeeManagement.Repositories.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EmployeeManagement.Repositories.DB_Context;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.WebAPI.Services;
using EmployeeManagement.DTOs.SharedModel;
using EmployeeManagement.DTOs.User;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDb; //for calling db via ef
        private readonly TokkenService _tokkenService;
        private readonly ILogger<DesignationController> _logger;
        public AuthController(ApplicationDbContext _applicationDb, TokkenService _tokkenService, ILogger<DesignationController> logger)
        {
            this._applicationDb = _applicationDb;
            this._tokkenService = _tokkenService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin,HR")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (await _applicationDb.users.AnyAsync(u => u.Email == dto.Email))
                    return BadRequest("Email already exists");

                var currentUserRole = User?.FindFirst(ClaimTypes.Role)?.Value;
                if (currentUserRole == "HR" && dto.Role != UserRoles.Employee)
                    return Forbid("HR can only register Employees");
                if (currentUserRole == "Employee")
                    return Forbid("Employee cannot register users");

                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Role = dto.Role,
                    IsActive = true //at the time of new user sign up, isactive is always true.
                };

                var hasher = new PasswordHasher<User>();
                user.PasswordHash = hasher.HashPassword(user, dto.Password);

                _applicationDb.users.Add(user);
                await _applicationDb.SaveChangesAsync();

                return Ok("User registered successfully");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while Registering User.");
                return BadRequest("Could not register user due to a database error.");
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _applicationDb.users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !user.IsActive)
                return Unauthorized("Invalid credentials or inactive user");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid password");

            var token = _tokkenService.GenerateToken(user);
            return Ok(new { token });
        }
        
    }
}
