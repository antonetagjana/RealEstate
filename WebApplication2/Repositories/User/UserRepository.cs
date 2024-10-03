using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.models;

namespace WebApplication2.Repositories.User;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<models.User?> GetByIdAsync(Guid userId)
    {
        return await dbContext.Users.Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Notifications)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }
    public async Task saveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<models.User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task AddAsync(models.User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(models.User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId)
    {
        var user = await GetByIdAsync(userId);
        if (user != null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }

   
}