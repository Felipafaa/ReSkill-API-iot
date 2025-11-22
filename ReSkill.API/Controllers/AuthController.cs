using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReSkill.API.Data;
using ReSkill.API.Models;

namespace ReSkill.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Usuário já cadastrado.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created("", user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginDetails)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDetails.Email && u.Password == loginDetails.Password);

            if (user == null)
            {
                return Unauthorized("Email ou senha inválidos.");
            }
            return Ok(user);
        }
    }
}