using WebApplication2.DTOs;
using WebApplication2.Repositories.Property;
using WebApplication2.models;

namespace WebApplication2.Services.Property;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
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
}