using WebApplication2.DTOs;
using WebApplication2.Repositories.Role;

namespace WebApplication2.Services.Role;
using models;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public Task<Role> GetRoleByIdAsync(Guid roleId)
    {
        return roleRepository.GetByIdAsync(roleId);
    }

    public Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return roleRepository.GetAllAsync();
    }

    public Task AddRoleAsync(Role role)
    {
        return roleRepository.AddAsync(role);
    }

    public async Task UpdateRoleAsync(Guid roleId, RoleUpdateDTO roleUpdateDto)
    {
        Role role = await GetRoleByIdAsync(roleId);
        
        if (roleId != role.RoleId) 
          throw new KeyNotFoundException($"Role with ID {roleId} not found.");
        
        role.RoleName = roleUpdateDto.RoleName;
        await roleRepository.UpdateAsync(role);
        // return null;
    }

    public Task DeleteRoleAsync(Guid roleId)
    {
        return roleRepository.DeleteAsync(roleId);
    }
}