using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data; 
using WebApplication2.models; 

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PronaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PronaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Prona
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prona>>> GetPronat()
        {
            return await _context.Properties.ToListAsync();
        }

        // GET: api/Prona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prona>> GetProna(Guid id)
        {
            var prona = await _context.Properties.FindAsync(id);

            if (prona == null)
            {
                return NotFound();
            }

            return prona;
        }

        // PUT: api/Prona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProna(Guid id, Prona prona)
        {
            if (id != prona.PropertyId)
            {
                return BadRequest();
            }

            _context.Entry(prona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PronaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prona
        [HttpPost]
        public async Task<ActionResult<Prona>> PostProna(Prona prona)
        {
            _context.Properties.Add(prona);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProna), new { id = prona.PropertyId }, prona);
        }

        // DELETE: api/Prona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProna(Guid id)
        {
            var prona = await _context.Properties.FindAsync(id);
            if (prona == null)
            {
                return NotFound();
            }

            _context.Properties.Remove(prona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PronaExists(Guid id)
        {
            return _context.Properties.Any(e => e.PropertyId == id);
        }
    }
}
