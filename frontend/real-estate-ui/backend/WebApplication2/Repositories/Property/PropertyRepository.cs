using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.models;

namespace WebApplication2.Repositories.Property;

public class PropertyRepository(ApplicationDbContext dbContext) : IPropertyRepository
{
    public async Task<Prona?> GetByIdAsync(Guid propertyId)
    {
        return await dbContext.Properties.Include(p => p.Photos)
            .Include(p => p.Reservations)
            .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
    }

    public async Task<IEnumerable<Prona>> GetAllAsync()
    {
        return await dbContext.Properties.ToListAsync();
    }

    public async Task AddAsync(Prona property)
    {
        await dbContext.Properties.AddAsync(property);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Prona property)
    {
        dbContext.Properties.Update(property);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid propertyId)
    {
        var property = await GetByIdAsync(propertyId);
        if (property != null)
        {
            dbContext.Properties.Remove(property);
            await dbContext.SaveChangesAsync();
        }
    }
}