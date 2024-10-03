using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Services.User;
using WebApplication2.Services.Property;
using WebApplication2.models;
using WebApplication2.Services.Role;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IPropertyService propertyService,IRoleService roleService)
        {
            _userService = userService;
            _propertyService = propertyService;
        }
        
        [Authorize(Policy = "MustBeAdminOrUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
    
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    
            // Kontrollo nëse është Admin ose po kërkon profilin e vet
            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(user);
            
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            if (await _userService.UserExists(userCreateDto.Email))
            {
                return BadRequest("User already exists.");
            }
            
            // Gjej rolin "Buyer" nga tabela e Roleve
            var roleName = userCreateDto.Role.ToLower(); 
            if (roleName != "buyer" && roleName != "seller")
            {
                return NotFound("Role not found");
            }

            var role = await _roleService.FindRoleByEmailAsync(roleName);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            var user = new User
            {
                FullName = userCreateDto.FullName,
                Email = userCreateDto.Email,
                PasswordHash = userCreateDto.PasswordHash,
                PhoneNumber = userCreateDto.PhoneNumber,
            };
            // Lidh përdoruesin me rolin
            user.UserRoles = new List<UserRole>
            {
                new UserRole { RoleId = role.RoleId, UserId = user.UserId }
            };
            
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }
        
        [Authorize(Policy = "AuthenticatedUserPolicy")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.UserId) return BadRequest();

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }
        
        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

    }
}
