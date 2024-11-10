using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Repositories.PropertyPhoto;
using models;

public class PropertyPhotoRepository(ApplicationDbContext dbContext) : IPropertyPhotoRepository
{
    public async Task<PropertyPhoto?> GetByIdAsync(Guid photoId)
    {
        return await dbContext.PropertyPhotos.Include(p => p.Property)
            .FirstOrDefaultAsync(p => p.PhotoId == photoId);
    }

    public async Task<IEnumerable<PropertyPhoto>> GetAllAsync()
    {
        return await dbContext.PropertyPhotos.ToListAsync();
    }

    public async Task AddAsync(PropertyPhoto propertyPhoto)
    {
        await dbContext.PropertyPhotos.AddAsync(propertyPhoto);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(PropertyPhoto propertyPhoto)
    {
        dbContext.PropertyPhotos.Update(propertyPhoto);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid photoId)
    {
        var photo = await GetByIdAsync(photoId);
        if (photo != null)
        {
            dbContext.PropertyPhotos.Remove(photo);
            await dbContext.SaveChangesAsync();
        }
    }
}