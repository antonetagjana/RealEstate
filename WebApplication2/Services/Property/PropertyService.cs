using WebApplication2.DTOs;
using WebApplication2.Repositories.Property;
using WebApplication2.models;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Services.Property;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly ApplicationDbContext _context;

    public PropertyService(IPropertyRepository propertyRepository, ApplicationDbContext context)
    {
        _propertyRepository = propertyRepository;
        _context = context;
    }
    
    public async Task<int> GetPropertyCountAsync()
    {
        return await _context.Properties.CountAsync();
    }

    public Task<Prona?> GetByIdAsync(Guid propertyId)
    {
        return _propertyRepository.GetByIdAsync(propertyId);
    }

    public Task<IEnumerable<Prona>> GetAllAsync()
    {
        return _propertyRepository.GetAllAsync();
    }

    public Task AddPropertyAsync(PropertyCreateDTO propertyCreateDto)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Prona>> GetPropertiesByLocationAsync(string location)
    {
        return await _propertyRepository.GetPropertiesByLocationAsync(location);
    }



    public Task AddPropertyAsync(Prona property)
    {
        return _propertyRepository.AddAsync(property);
    }

    public Task UpdateAsync(Prona property)
    {
        return _propertyRepository.UpdateAsync(property);
    }

    public Task DeleteAsync(Guid propertyId)
    {
        return _propertyRepository.DeleteAsync(propertyId);
    }

    public async Task<IEnumerable<Prona>> GetPropertiesBySellerIdAsync(Guid sellerId)
    {
        return await _propertyRepository.GetPropertiesBySellerIdAsync(sellerId);
    }
    
    public async Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(
        decimal? minPrice, 
        decimal? maxPrice, 
        string? category, 
        string? location, 
        int? floors)
    {
        // Thirr metodën nga repository për të marrë të dhënat e filtruara
        return await _propertyRepository.GetFilteredPropertiesAsync(minPrice, maxPrice,category , location, floors);
    }

}
