namespace WebApplication2.Services.User;
using models;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<models.User>> GetAllUsersAsync();
    Task AddUserAsync(models.User user);
    Task UpdateUserAsync(models.User user);
    Task DeleteUserAsync(Guid userId);
}