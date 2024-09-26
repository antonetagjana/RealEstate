using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Repositories.UserRole;
using models;

public class UserRoleRepository(ApplicationDbContext dbContext) : IUserRoleRepository
{
    public async Task<UserRole?> GetByIdAsync(Guid userId, Guid roleId)
    {
        return await dbContext.UserRoles
            .Include(ur => ur.User)
            .Include(ur => ur.Role)
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await dbContext.UserRoles.Include(ur => ur.User)
            .Include(ur => ur.Role)
            .ToListAsync();
    }

    public async Task AddAsync(UserRole userRole)
    {
        await dbContext.UserRoles.AddAsync(userRole);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId, Guid roleId)
    {
        var userRole = await GetByIdAsync(userId, roleId);
        if (userRole != null)
        {
            dbContext.UserRoles.Remove(userRole);
            await dbContext.SaveChangesAsync();
        }
    }
}