namespace WebApplication2.Repositories.Role;
using models;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(Guid roleId);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(Guid roleId);  
}