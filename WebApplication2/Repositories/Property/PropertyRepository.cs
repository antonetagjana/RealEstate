using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.models;

namespace WebApplication2.Repositories.Property;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PropertyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task saveChanges()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Prona>> GetPropertiesBySellerIdAsync(Guid sellerId)
    {
        // Merr të gjitha pronat ku UserId është sellerId
        return await _dbContext.Properties
            .Where(p => p.UserId == sellerId)  // Filtro bazuar në ID-në e Seller-it
            .ToListAsync();
    }

    public async Task<Prona?> GetByIdAsync(Guid propertyId)
    {
        return await _dbContext.Properties
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
    }


    public async Task<IEnumerable<Prona>> GetAllAsync()
    {
        return await _dbContext.Properties
            .Include(p => p.Photos) // Ensure photos are loaded
            .ToListAsync();
    }


    public async Task AddAsync(Prona property)
    {
        await _dbContext.Properties.AddAsync(property);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Prona property)
    {
        _dbContext.Properties.Update(property);
        await _dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<Prona>> GetPropertiesByLocationAsync(string location)
    {
        try
        {
            // Convert search term to lowercase for case-insensitive search
            location = location.ToLower();

            return await _dbContext.Properties
                .Include(p => p.Photos)
                .Where(p => p.Location.ToLower().Contains(location))
                .Where(p => p.IsAvailable)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            // Log the error
            throw;
        }
    }




    public async Task DeleteAsync(Guid propertyId)
    {
        var property = await GetByIdAsync(propertyId);
        if (property != null)
        {
            _dbContext.Properties.Remove(property);
            await _dbContext.SaveChangesAsync();
        }
    }
     public async Task<IEnumerable<Prona>> GetFilteredPropertiesAsync(
            decimal? minPrice, 
            decimal? maxPrice, 
            string? category, 
            string? location, 
            int? floors)
        {
            // Krijo një query të bazuar në filtrat e dhënë
            var query = _dbContext.Properties.AsQueryable();
    
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
    
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);
    
            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);
    
            if (!string.IsNullOrEmpty(location))
                query = query.Where(p => p.Location.Contains(location));
    
            if (floors.HasValue)
                query = query.Where(p => p.Floors == floors.Value);
    
            // Kthe rezultatin
            return await query.ToListAsync();
        }
}