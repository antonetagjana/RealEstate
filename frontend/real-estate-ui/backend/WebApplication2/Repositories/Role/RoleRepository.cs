using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Repositories.Role;
using models;

public class RoleRepository: IRoleRepository
{
    
    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Role> GetByIdAsync(Guid roleId)
    {
        var role = await _dbContext.Roles.Include(r => r.UserRoles)
            .FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        }

        return role;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _dbContext.Roles.Include(r => r.UserRoles).ToListAsync();
    }

    public async Task AddAsync(Role role)
    {
        await _dbContext.Roles.AddAsync(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role role)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid roleId)
    {
        var role = await GetByIdAsync(roleId);
        if (role != null)
        {
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
        }
    }
}