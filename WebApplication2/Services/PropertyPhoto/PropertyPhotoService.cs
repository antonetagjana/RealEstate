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
    
    public async Task<string> SavePhotoAsync(IFormFile file)
    {
        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        // Krijo dosjen nëse nuk ekziston
        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        // Rruga për skedarin e ri
        var filePath = Path.Combine(imagesPath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Kthe URL-në relative për të arritur skedarin
        return $"/images/{file.FileName}";
    }

}
