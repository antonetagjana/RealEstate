using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.models;
using WebApplication2.Services.Property;
using WebApplication2.Services.PropertyPhoto;
using WebApplication2.Services.User;

namespace WebApplication2.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly KafkaService _kafkaService;
        private readonly PropertyPhotoService _photoService;

        public PropertyController(IPropertyService propertyService, KafkaService kafkaService, IUserService userService, PropertyPhotoService photoService)
        {
            _propertyService = propertyService;
            _userService = _userService;
            _kafkaService = kafkaService;
            _photoService = photoService;
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(Guid id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null) return NotFound();
            
         
            // var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // if (property.UserId != Guid.Parse(currentUserId) && !User.IsInRole("Admin"))
            // {
            //     return Forbid("Nuk keni të drejtë të shihni këtë pronë.");
            // }
            
            return Ok(property);
        }
         [HttpGet("count")]
            public async Task<IActionResult> GetPropertyCount()
            {
                var count = await _propertyService.GetPropertyCountAsync();
                return Ok(count);
            }
            
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProperties()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"Current User ID: {currentUserId}");
            Console.WriteLine($"User Roles: {string.Join(", ", User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value))}");

            if (User.IsInRole("Seller"))
            {
              
                var properties = await _propertyService.GetPropertiesBySellerIdAsync(Guid.Parse(currentUserId));
                Console.WriteLine($"Properties for Seller: {properties.Count()}");
                return Ok(properties);
            }

        
            var allProperties = await _propertyService.GetAllAsync();
            Console.WriteLine($"All Properties: {allProperties.Count()}");
            return Ok(allProperties);
        }
            // Merr numrin e pronave të listuara nga seller-i i loguar
            /*
            [HttpGet("count-listed")]
            public async Task<IActionResult> GetListedPropertiesCount()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Merr ID e seller-it nga tokeni
                var count = await _propertyService.Properties
                    .Where(p => p.userId == userId)
                    .CountAsync();
        
                return Ok(count);
            } */

            [AllowAnonymous]
            [HttpGet("filter")]
            public async Task<IActionResult> GetFilteredProperties(
                [FromQuery] decimal? minPrice,
                [FromQuery] decimal? maxPrice,
                [FromQuery] string? category,
                [FromQuery] string? location,
                [FromQuery] int? floors)
            {
                try
                {
                    Console.WriteLine($"Incoming filter parameters: minPrice={minPrice}, maxPrice={maxPrice}, category={category}, location={location}, floors={floors}");
        
                    var properties = await _propertyService.GetFilteredPropertiesAsync(minPrice, maxPrice, category, location, floors);
        
                    if (properties == null || !properties.Any())
                    {
                        Console.WriteLine("No matching properties found.");
                        return NotFound("No matching properties found.");
                    }

                    Console.WriteLine($"Properties returned: {properties.Count()}");
                    return Ok(properties);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return StatusCode(500, "An internal error occurred.");
                }
            }

        [AllowAnonymous]
        // [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProperty([FromForm] PropertyCreateDTO propertyCreateDto, IFormFile photo)
        {
            // Create the property
            var property = new Prona
            {
                // UserId = propertyCreateDto.UserId,
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

            // Save property to database
            await _propertyService.AddPropertyAsync(property);

            // If photo exists, handle photo upload
            if (photo != null)
            {
                // Process photo, e.g., save to server and generate URL
                var photoUrl = await SavePhotoAsync(photo);
        
                var propertyPhoto = new PropertyPhoto
                {
                    PhotoUrl = photoUrl,
                    PropertyId = property.PropertyId,
                };

                await _photoService.AddAsync(propertyPhoto);
            }

            // await _kafkaService.SendMessageAsync($"Property created with ID: {property.PropertyId}");

            return CreatedAtAction(nameof(GetPropertyById), new { id = property.PropertyId }, property);
        }

        
       // [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] Prona prona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound("Property not found");

           
            if (id != property.PropertyId)
                return BadRequest("Property ID mismatch");

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            
            if (property.UserId != Guid.Parse(currentUserId) && !User.IsInRole("Admin"))
            {
                return Forbid("You are not authorized to update this property."); 
            }

          
            property.Title = prona.Title;
            property.Description = prona.Description;
            property.Location = prona.Location;
            property.Price = prona.Price;
            property.SurfaceArea = prona.SurfaceArea;
            property.IsAvailable = prona.IsAvailable;
            property.IsPromoted = prona.IsPromoted;
            property.Floors= prona.Floors;
            

            await _propertyService.UpdateAsync(property);
            return NoContent();
        }
        
       // [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound();

            await _propertyService.DeleteAsync(id);
            return NoContent();
        }
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchProperties([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest("Location is required.");
            }

            var properties = await _propertyService.GetPropertiesByLocationAsync(location);

            if (!properties.Any())
            {
                return NotFound("No properties found in this location.");
            }

            return Ok(properties);
        }


        
        [AllowAnonymous]
        [HttpPost("{propertyId}/upload-photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadPhoto(Guid propertyId, [FromForm] IFormFile file)
        {
            // Kontrollo nëse prona ekziston
            var property = await _propertyService.GetByIdAsync(propertyId);
            if (property == null)
                return NotFound("Property not found");

            // Kontrollo nëse skedari është valid
            if (file == null || file.Length == 0)
                return BadRequest("Invalid photo file");

            // Ruaj foton dhe merr URL-në e saj
            var photoUrl = await _photoService.SavePhotoAsync(file);

            // Krijo një objekt PropertyPhoto
            var photo = new PropertyPhoto
            {
                PhotoId = Guid.NewGuid(), // Gjenero ID unike për foton
                PropertyId = propertyId,
                PhotoUrl = photoUrl,
                CreatedDate = DateTime.UtcNow
            };

            // Ruaj foton në bazën e të dhënave
            await _photoService.AddAsync(photo);

            // Kthe përgjigjen
            return Ok(photo);
        }

        
        [HttpPost]
        public async Task<string> SavePhotoAsync(IFormFile file)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            // Krijo dosjen nëse nuk ekziston
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            // Rruga për skedarin e ri
            var filePath = Path.Combine(imagesPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Kthe URL-në relative për të arritur skedarin
            return $"/images/{file.FileName}";
        }

    }
}
