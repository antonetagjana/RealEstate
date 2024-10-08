using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTOs;
using WebApplication2.Repositories.Role;

namespace WebApplication2.Services.Role;
using models;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly ApplicationDbContext _dbContext;

    public RoleService(IRoleRepository roleRepository, ApplicationDbContext dbContext)
    {
        _roleRepository = roleRepository;
        _dbContext = dbContext;
    }

    public Task<Role> GetRoleByIdAsync(Guid roleId)
    {
        return _roleRepository.GetByIdAsync(roleId);
    }

    public Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return _roleRepository.GetAllAsync();
    }

    public Task AddRoleAsync(Role role)
    {
        return _roleRepository.AddAsync(role);
    }

    public async Task UpdateRoleAsync(Guid roleId, RoleUpdateDTO roleUpdateDto)
    {
        Role role = await GetRoleByIdAsync(roleId);
        
        if (role == null) 
          throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        
        role.RoleName = roleUpdateDto.RoleName;
        await _roleRepository.UpdateAsync(role);
    }

    public Task DeleteRoleAsync(Guid roleId)
    {
        return _roleRepository.DeleteAsync(roleId);
    }

    public async Task<Role?> FindRoleByEmailAsync(string email)
    {
        var user = await _dbContext.Users.Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) return null;

        return user.UserRoles.Select(ur => ur.Role).FirstOrDefault(); // Kthen rolin e parë të përdoruesit
    }
    public async Task<Role?> FindRoleByRoleNameAsync(string roleName)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }

    
}
