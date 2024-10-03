using WebApplication2.DTOs;

namespace WebApplication2.Services.Role;
using models;

public interface IRoleService
{
    Task<Role> GetRoleByIdAsync(Guid roleId);
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task AddRoleAsync(Role role);
    Task UpdateRoleAsync(Guid roleId, RoleUpdateDTO roleUpdateDto);
    Task DeleteRoleAsync(Guid roleId);
    Task<Role?> FindRoleByEmailAsync(string email);
}