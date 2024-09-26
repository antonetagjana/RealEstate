using WebApplication2.Repositories.PropertyPhoto;

namespace WebApplication2.Services.PropertyPhoto;

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using models;

public class PropertyPhotoService(IPropertyPhotoRepository propertyPhotoRepository) : IPropertyPhotoService
{
    public Task<PropertyPhoto?> GetByIdAsync(Guid photoId)
    {
        return propertyPhotoRepository.GetByIdAsync(photoId);
    }

    public Task<IEnumerable<PropertyPhoto>> GetAllAsync()
    {
        return propertyPhotoRepository.GetAllAsync();
    }

    public Task AddAsync(PropertyPhoto propertyPhoto)
    {
        return propertyPhotoRepository.AddAsync(propertyPhoto);
    }

    public Task UpdateAsync(PropertyPhoto propertyPhoto)
    {
        return propertyPhotoRepository.UpdateAsync(propertyPhoto);
    }

    public Task DeleteAsync(Guid photoId)
    {
        return propertyPhotoRepository.DeleteAsync(photoId);
    }
}
