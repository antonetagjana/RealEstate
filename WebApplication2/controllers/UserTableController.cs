using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.models;


namespace WebApplication2.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class UserTableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserTableController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserTable
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTable>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserTable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTable>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/UserTable/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UserTable user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/UserTable
        [HttpPost]
        public async Task<ActionResult<UserTable>> PostUser(UserTable user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // DELETE: api/UserTable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}