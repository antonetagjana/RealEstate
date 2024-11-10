using WebApplication2.models;

namespace WebApplication2.Repositories.Property;

public interface IPropertyRepository
{
    Task<Prona?> GetByIdAsync(Guid propertyId);
    Task<IEnumerable<Prona>> GetAllAsync();
    Task AddAsync(Prona property);
    Task UpdateAsync(Prona property);
    Task DeleteAsync(Guid propertyId);
}