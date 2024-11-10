namespace WebApplication2.Repositories.UserRole;
using models;

public interface IUserRoleRepository
{
    Task<UserRole?> GetByIdAsync(Guid userId, Guid roleId);
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task AddAsync(UserRole userRole);
    Task DeleteAsync(Guid userId, Guid roleId);   
}