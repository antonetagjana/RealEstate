using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.Services.Property;

namespace WebApplication2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PropertyController(IPropertyService propertyService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropertyById(Guid id)
    {
        var property = await propertyService.GetByIdAsync(id);
        if (property == null) return NotFound();
        return Ok(property);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        var properties = await propertyService.GetAllAsync();
        return Ok(properties);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty([FromBody] Prona property)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await propertyService.AddAsync(property);
        return CreatedAtAction(nameof(GetPropertyById), new { id = property.PropertyId }, property);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] Prona property)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != property.PropertyId)
            return BadRequest("Property ID mismatch");

        await propertyService.UpdateAsync(property);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        var property = await propertyService.GetByIdAsync(id);
        if (property == null)
            return NotFound();

        await propertyService.DeleteAsync(id);
        return NoContent();
    }
}