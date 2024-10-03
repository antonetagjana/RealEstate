using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.models;
using WebApplication2.Services.Property;
using WebApplication2.Services.User;

namespace WebApplication2.Controllers
{   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
            _userService = _userService;
        }
        [Authorize(Policy = "AuthenticatedUserPolicy")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(Guid id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null) return NotFound();
            
            // Kontrollo nëse përdoruesi është Seller dhe ka akses në këtë pronë
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (property.UserId != Guid.Parse(currentUserId) && !User.IsInRole("Admin"))
            {
                return Forbid("Nuk keni të drejtë të shihni këtë pronë.");
            }
            
            return Ok(property);
        }
        [Authorize(Policy = "AuthenticatedUserPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Seller"))
            {
                // Filtro pronat vetëm për Seller-in e loguar
                var properties = await _propertyService.GetPropertiesBySellerIdAsync(Guid.Parse(currentUserId));
                return Ok(properties);
            }

            // Admin ose Buyer mund të shohin të gjitha pronat
            var allProperties = await _propertyService.GetAllAsync();
            return Ok(allProperties);
        }
        [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpPost]
        public async Task<IActionResult> CreateProperty( PropertyCreateDTO propertyCreateDto)
        {
            var property = new Prona
            {
                UserId = propertyCreateDto.UserId,
                Title = propertyCreateDto.Title,
                Description = propertyCreateDto.Description,
                Category = propertyCreateDto.Category,
                Location = propertyCreateDto.Location,
                Price = propertyCreateDto.Price,
                SurfaceArea = propertyCreateDto.SurfaceArea,
                Floors = propertyCreateDto.Floors,
                IsAvailable = propertyCreateDto.IsAvailable,
                IsPromoted = propertyCreateDto.IsPromoted,

            };

            await _propertyService.AddPropertyAsync(property);

            return CreatedAtAction(nameof(GetPropertyById), new { id = property.PropertyId }, property);
        }
        
        [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] Prona prona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Merr pronën nga databaza për ta kontrolluar
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound("Property not found");

            // Kontrollo nëse Property ID përputhet
            if (id != property.PropertyId)
                return BadRequest("Property ID mismatch");

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Merr ID-në e përdoruesit të loguar

            // Kontrollo nëse përdoruesi është admin ose pronari i pronës
            if (property.UserId != Guid.Parse(currentUserId) && !User.IsInRole("Admin"))
            {
                return Forbid("You are not authorized to update this property."); // Ndalim për përdoruesit që nuk kanë të drejta
            }

            // Përditëso pronën
            property.Title = prona.Title;
            property.Description = prona.Description;
            property.Location = prona.Location;
            property.Price = prona.Price;
            property.SurfaceArea = prona.SurfaceArea;
            property.IsAvailable = prona.IsAvailable;
            property.IsPromoted = prona.IsPromoted;
            property.Floors= prona.Floors;
            // Shënim: Vendos të gjitha fushat e tjera që dëshiron të përditësohen

            await _propertyService.UpdateAsync(property);
            return NoContent();
        }
        
        [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound();

            await _propertyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
