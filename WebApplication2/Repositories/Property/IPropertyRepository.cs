using WebApplication2.models;

namespace WebApplication2.Repositories.Property;

public interface IPropertyRepository
{
    Task<Prona?> GetByIdAsync(Guid propertyId);
    Task<IEnumerable<Prona>> GetAllAsync();
    Task AddAsync(Prona property);
    Task UpdateAsync(Prona property);
    Task DeleteAsync(Guid propertyId);
    Task saveChanges();
    Task<IEnumerable<Prona>> GetPropertiesBySellerIdAsync(Guid sellerId);
    Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(decimal? minPrice, decimal? maxPrice, string? propertyType, string? location, int? rooms);
    Task<IEnumerable<Prona>> GetPropertiesByLocationAsync(string location);

}