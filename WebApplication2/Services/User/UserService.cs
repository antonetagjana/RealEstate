using WebApplication2.Repositories.User;

namespace WebApplication2.Services.User;
using models;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<User?> GetUserByIdAsync(Guid userId)
    {
        return userRepository.GetByIdAsync(userId);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return userRepository.GetAllAsync();
    }
  
    public Task AddUserAsync(User user)
    {
        return userRepository.AddAsync(user);
    }

    public Task UpdateUserAsync(User user)
    {
        return userRepository.UpdateAsync(user);
    }

    public Task DeleteUserAsync(Guid userId)
    {
        return userRepository.DeleteAsync(userId);
    }
}