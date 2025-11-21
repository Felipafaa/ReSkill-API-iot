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
    public class SessionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SessionsController> _logger;

        public SessionsController(AppDbContext context, ILogger<SessionsController> logger)
        {
            _context = context;
            _logger = logger; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Buscando sessões de estudo - Página {Page}", page);

            var totalItems = await _context.StudySessions.CountAsync();

            var sessions = await _context.StudySessions
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = sessions.Select(s => new
            {
                Data = s,
                Links = new[]
                {
                    new { rel = "self", href = $"/api/v1/sessions/{s.Id}", method = "GET" },
                    new { rel = "update", href = $"/api/v1/sessions/{s.Id}", method = "PUT" },
                    new { rel = "delete", href = $"/api/v1/sessions/{s.Id}", method = "DELETE" }
                }
            });

            var response = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Items = result
            };

            return Ok(response); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var session = await _context.StudySessions.FindAsync(id);

            if (session == null) return NotFound(); 

            var resource = new
            {
                Data = session,
                Links = new[]
                {
                    new { rel = "self", href = $"/api/v1/sessions/{id}", method = "GET" },
                    new { rel = "delete", href = $"/api/v1/sessions/{id}", method = "DELETE" }
                }
            };

            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudySession session)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 

            _context.StudySessions.Add(session);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Nova sessão criada com ID {Id}", session.Id);

            return CreatedAtAction(nameof(GetById), new { id = session.Id, version = "1" }, session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StudySession session)
        {
            if (id != session.Id) return BadRequest();

            _context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.StudySessions.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _context.StudySessions.FindAsync(id);
            if (session == null) return NotFound();

            _context.StudySessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}