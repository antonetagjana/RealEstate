using WebApplication2.models;

namespace WebApplication2.Repositories.User;

public interface IUserRepository
{
    Task<models.User?> GetByIdAsync(Guid userId);
    Task<IEnumerable<models.User>> GetAllAsync();
    Task AddAsync(models.User user);
    
    Task UpdateAsync(models.User user);
    Task DeleteAsync(Guid userId);
    Task saveChangesAsync();
}