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
        return await _dbContext.Properties.FindAsync(propertyId);
    }

    public async Task<IEnumerable<Prona>> GetAllAsync()
    {
        return await _dbContext.Properties.ToListAsync();
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

    public async Task DeleteAsync(Guid propertyId)
    {
        var property = await GetByIdAsync(propertyId);
        if (property != null)
        {
            _dbContext.Properties.Remove(property);
            await _dbContext.SaveChangesAsync();
        }
    }
}