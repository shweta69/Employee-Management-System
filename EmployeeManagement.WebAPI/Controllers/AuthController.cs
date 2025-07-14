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

namespace EmployeeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDb; //for calling db via ef
        private readonly IConfiguration _config; //for accessing the jwt tokken
        public AuthController(ApplicationDbContext _applicationDb, IConfiguration _config)
        {
            this._applicationDb = _applicationDb;
            this._config = _config;
        }
        
    }
}
