using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.Services.UserRoles;

namespace WebApplication2.controllers;

[Authorize(Policy = "AdminPolicy")]
[ApiController]
[Route("api/[controller]")]
public class UserRoleController(IUserRolesService userRoleService) : ControllerBase
{
    [HttpGet("{userId}/{roleId}")]
    public async Task<IActionResult> GetUserRoleById(Guid userId, Guid roleId)
    {
        var userRole = await userRoleService.GetByIdAsync(userId, roleId);
        if (userRole == null) return NotFound();
        return Ok(userRole);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserRoles()
    {
        var userRoles = await userRoleService.GetAllAsync();
        return Ok(userRoles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserRole([FromBody] UserRole userRole)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await userRoleService.AddAsync(userRole);
        return CreatedAtAction(nameof(GetUserRoleById), new { userId = userRole.UserId, roleId = userRole.RoleId }, userRole);
    }

    [HttpDelete("{userId}/{roleId}")]
    public async Task<IActionResult> DeleteUserRole(Guid userId, Guid roleId)
    {
        var userRole = await userRoleService.GetByIdAsync(userId, roleId);
        if (userRole == null)
            return NotFound();

        await userRoleService.DeleteAsync(userId, roleId);
        return NoContent();
    }
}