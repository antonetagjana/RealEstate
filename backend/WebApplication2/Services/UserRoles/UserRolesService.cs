using WebApplication2.Repositories.UserRole;

namespace WebApplication2.Services.UserRoles;

using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using models;

public class UserRoleService(IUserRoleRepository userRoleRepository) : IUserRolesService
{
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;

    public Task<UserRole?> GetByIdAsync(Guid userId, Guid roleId)
    {
        return _userRoleRepository.GetByIdAsync(userId, roleId);
    }

    public Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return _userRoleRepository.GetAllAsync();
    }

    public Task AddAsync(UserRole userRole)
    {
        return _userRoleRepository.AddAsync(userRole);
    }

    public Task DeleteAsync(Guid userId, Guid roleId)
    {
        return _userRoleRepository.DeleteAsync(userId, roleId);
    }
}
