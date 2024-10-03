using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.models; 
using WebApplication2.Data;
using WebApplication2.DTOs;
using WebApplication2.Services.Role;

namespace WebApplication2.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateDTO roleCreateDto)
        {
            var role = new Role
            {
                RoleId = roleCreateDto.RoleId,
                RoleName = roleCreateDto.RoleName
            };
            await roleService.AddRoleAsync(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleId }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, RoleUpdateDTO roleUpdateDto)
        {
            await roleService.UpdateRoleAsync(id, roleUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }

}
