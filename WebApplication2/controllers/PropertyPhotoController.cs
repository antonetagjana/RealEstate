using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data; 
using WebApplication2.models;
using WebApplication2.Services.PropertyPhoto;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyPhotoController(IPropertyPhotoService propertyPhotoService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyPhotoById(Guid id)
        {
            var photo = await propertyPhotoService.GetByIdAsync(id);
            if (photo == null) return NotFound();
            return Ok(photo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPropertyPhotos()
        {
            var photos = await propertyPhotoService.GetAllAsync();
            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyPhoto(PropertyPhoto propertyPhoto)
        {
            await propertyPhotoService.AddAsync(propertyPhoto);
            return CreatedAtAction(nameof(GetPropertyPhotoById), new { id = propertyPhoto.PhotoId }, propertyPhoto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePropertyPhoto(Guid id, PropertyPhoto propertyPhoto)
        {
            if (id != propertyPhoto.PhotoId) return BadRequest();

            await propertyPhotoService.UpdateAsync(propertyPhoto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyPhoto(Guid id)
        {
            await propertyPhotoService.DeleteAsync(id);
            return NoContent();
        }
    }

}
