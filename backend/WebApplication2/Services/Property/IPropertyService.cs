namespace WebApplication2.Services.Property;
using models;

public interface IPropertyService
{
    Task<Prona?> GetByIdAsync(Guid propertyId);
    Task<IEnumerable<Prona>> GetAllAsync();
    Task AddAsync(Prona property);
    Task UpdateAsync(Prona property);
    Task DeleteAsync(Guid propertyId);
}