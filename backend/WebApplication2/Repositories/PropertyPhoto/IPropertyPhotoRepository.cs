namespace WebApplication2.Repositories.PropertyPhoto;
using models;

public interface IPropertyPhotoRepository
{
    Task<PropertyPhoto?> GetByIdAsync(Guid photoId);
    Task<IEnumerable<PropertyPhoto>> GetAllAsync();
    Task AddAsync(PropertyPhoto propertyPhoto);
    Task UpdateAsync(PropertyPhoto propertyPhoto);
    Task DeleteAsync(Guid photoId);
}