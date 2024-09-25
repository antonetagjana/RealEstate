using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data; 
using WebApplication2.models; 

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyPhotoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertyPhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PropertyPhoto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyPhoto>>> GetPropertyPhotos()
        {
            return await _context.PropertyPhotos.ToListAsync();
        }

        // GET: api/PropertyPhoto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyPhoto>> GetPropertyPhoto(Guid id)
        {
            var propertyPhoto = await _context.PropertyPhotos.FindAsync(id);

            if (propertyPhoto == null)
            {
                return NotFound();
            }

            return propertyPhoto;
        }

        // PUT: api/PropertyPhoto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropertyPhoto(Guid id, PropertyPhoto propertyPhoto)
        {
            if (id != propertyPhoto.PhotoId)
            {
                return BadRequest();
            }

            _context.Entry(propertyPhoto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyPhotoExists(id))
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

        // POST: api/PropertyPhoto
        [HttpPost]
        public async Task<ActionResult<PropertyPhoto>> PostPropertyPhoto(PropertyPhoto propertyPhoto)
        {
            _context.PropertyPhotos.Add(propertyPhoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPropertyPhoto), new { id = propertyPhoto.PhotoId }, propertyPhoto);
        }

        // DELETE: api/PropertyPhoto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyPhoto(Guid id)
        {
            var propertyPhoto = await _context.PropertyPhotos.FindAsync(id);
            if (propertyPhoto == null)
            {
                return NotFound();
            }

            _context.PropertyPhotos.Remove(propertyPhoto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyPhotoExists(Guid id)
        {
            return _context.PropertyPhotos.Any(e => e.PhotoId == id);
        }
    }
}
