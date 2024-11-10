namespace WebApplication2.Services.UserRoles;
using models;

public interface IUserRolesService
{
    Task<UserRole?> GetByIdAsync(Guid userId, Guid roleId);
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task AddAsync(UserRole userRole);
    Task DeleteAsync(Guid userId, Guid roleId);
}