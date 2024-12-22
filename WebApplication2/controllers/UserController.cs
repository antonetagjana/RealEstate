using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    // [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        private readonly IRoleService _roleService;
        private readonly EmailService _emailService;

        public UserController(IUserService userService, IPropertyService propertyService,IRoleService roleService,EmailService emailService)
        {
            _userService = userService;
            _propertyService = propertyService;
            _roleService= roleService;
            _emailService = emailService;
        }
        
        // [Authorize(Policy = "MustBeAdminOrUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
    
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    
            if (currentUserId != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(user);
            
        }
        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserCount()
        {
            var count = await _userService.GetUserCountAsync();
            Console.WriteLine($"Sending user count: {count}");
            return Ok(count);
        }

        
        // [Authorize(Policy = "AdminPolicy")]
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
            
            
            if (!IsPasswordValid(userCreateDto.Password))
            {
                return BadRequest(new { message = "Password does not meet security requirements. It must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit." });
            }
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            var user = new User
            {
                FullName = userCreateDto.FullName,
                Email = userCreateDto.Email,
                PasswordHash= hashedPassword,
                PhoneNumber = userCreateDto.PhoneNumber,
                Role = userCreateDto.Role
            };

          
            await _userService.AddUserAsync(user);
            
            // Gjenero token e konfirmimit dhe dërgo email
            // var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // await _emailService.SendConfirmationEmailAsync(user.UserId.ToString(), user.Email, confirmationToken);


            return Ok(new { message = "User registered successfully." });
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
            if (password.Length < 8) return false; 
            if (!password.Any(char.IsLower)) return false; 
            if (!password.Any(char.IsUpper)) return false; 
            if (!password.Any(char.IsDigit)) return false; 

            return true;
        }


    }
}
