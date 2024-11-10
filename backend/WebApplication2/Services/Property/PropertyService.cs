using WebApplication2.Repositories.Property;

namespace WebApplication2.Services.Property;

using WebApplication2.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class PropertyService(IPropertyRepository propertyRepository) : IPropertyService
{
    public Task<Prona?> GetByIdAsync(Guid propertyId)
    {
        return propertyRepository.GetByIdAsync(propertyId);
    }

    public Task<IEnumerable<Prona>> GetAllAsync()
    {
        return propertyRepository.GetAllAsync();
    }

    public Task AddAsync(Prona property)
    {
        return propertyRepository.AddAsync(property);
    }

    public Task UpdateAsync(Prona property)
    {
        return propertyRepository.UpdateAsync(property);
    }

    public Task DeleteAsync(Guid propertyId)
    {
        return propertyRepository.DeleteAsync(propertyId);
    }
}
