using WebApplication2.DTOs;
using WebApplication2.models;
using WebApplication2.models;

namespace WebApplication2.Services.Property
{
    public interface IPropertyService
    {
        Task<Prona?> GetByIdAsync(Guid propertyId);
        Task<IEnumerable<Prona>> GetAllAsync();
        Task AddPropertyAsync(PropertyCreateDTO propertyCreateDto);
        Task AddPropertyAsync(Prona property);
        Task UpdateAsync(Prona property);
        Task DeleteAsync(Guid propertyId);
        Task<IEnumerable<Prona>> GetPropertiesBySellerIdAsync(Guid sellerId);
        Task<int> GetPropertyCountAsync();
        Task<IEnumerable<Prona>> GetPropertiesByLocationAsync(string location);

        Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(decimal? minPrice, decimal? maxPrice, string? propertyType, string? location, int? rooms);
    }
}