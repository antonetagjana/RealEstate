using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.DTOs;
using WebApplication2.Repositories.User;

namespace WebApplication2.Services.User;
using models;


public class UserService(IUserRepository userRepository,ApplicationDbContext dbContext) : IUserService



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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UserExists(object email)
    {
        return await dbContext.Users.AnyAsync(u => u.Email == email.ToString());
    }
    
    // public async Task AddMultiplePropertiesToUserAsync(Guid userId, List<PropertyCreateDTO> propertyCreateDtos)
    // {
    //     var properties = new List<Prona>();
    //
    //     foreach (var propertyDto in propertyCreateDtos)
    //     {
    //         var property = new Prona
    //         {
    //             UserId = userId,
    //             Title = propertyDto.Title,
    //             Description = propertyDto.Description,
    //             Category = propertyDto.Category,
    //             Location = propertyDto.Location,
    //             Price = propertyDto.Price,
    //             Floors = propertyDto.Floors,
    //             SurfaceArea = propertyDto.SurfaceArea,
    //         };
    //         properties.Add(property);
    //
    //     }
    //     await userRepository
    // }
    
 
        
    }
