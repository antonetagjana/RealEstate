using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly EmailService _emailService;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, IPropertyService propertyService,IRoleService roleService,EmailService emailService,UserManager<User> userManager)
        {
            _userService = userService;
            _propertyService = propertyService;
            _roleService= roleService;
            _emailService = emailService;
            _userManager = userManager;
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
            
            // Kontrollo nëse fjalëkalimi përmbush kriteret e sigurisë
            if (!IsPasswordValid(userCreateDto.Password))
            {
                return BadRequest("Password does not meet security requirements. It must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.");
            }

            if (await _userService.UserExists(userCreateDto.Email))
            {
                return BadRequest("User already exists.");
            }
            
            var user = new User
            {
                FullName = userCreateDto.FullName,
                Email = userCreateDto.Email,
                Password= userCreateDto.Password,
                PhoneNumber = userCreateDto.PhoneNumber,
            };
            
            // Krijo listën e roleve për përdoruesin
            var userRoles = new List<UserRole>();

            foreach (var roleName in userCreateDto.Roles)
            {
                var role = await _roleService.FindRoleByRoleNameAsync(roleName.ToLower());
                if (role == null)
                {
                    return NotFound($"Role '{roleName}' not found.");
                }

                userRoles.Add(new UserRole { RoleId = role.RoleId, UserId = user.UserId });
            }

            user.UserRoles = userRoles; // Lidh të gjitha rolet me përdoruesin

            // Ruaj përdoruesin e ri dhe rolet e tij
            await _userService.AddUserAsync(user);
            
            // Gjenero token-in e konfirmimit dhe dërgo email-in
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailService.SendConfirmationEmailAsync(user.UserId.ToString(), user.Email, confirmationToken);


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
        
        private bool IsPasswordValid(string password)
        {
            if (password.Length < 8) return false; // Minimumi 8 karaktere
            if (!password.Any(char.IsLower)) return false; // Të paktën një shkronjë të vogël
            if (!password.Any(char.IsUpper)) return false; // Të paktën një shkronjë të madhe
            if (!password.Any(char.IsDigit)) return false; // Të paktën një numër

            return true;
        }


    }
}
