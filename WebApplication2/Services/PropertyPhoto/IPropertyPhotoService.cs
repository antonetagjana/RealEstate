namespace WebApplication2.Services.PropertyPhoto;
using models;

public interface IPropertyPhotoService
{
    Task<PropertyPhoto?> GetByIdAsync(Guid photoId);
    Task<IEnumerable<PropertyPhoto>> GetAllAsync();
    Task AddAsync(PropertyPhoto propertyPhoto);
    Task UpdateAsync(PropertyPhoto propertyPhoto);
    Task DeleteAsync(Guid photoId);  
    Task<string> SavePhotoAsync(IFormFile file);

}