using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.Services.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApplication2.DTOs;
using WebApplication2.Services;
using BCrypt.Net;



namespace WebApplication2.controllers;
[ApiController]
[Route("api/[controller]")]

public class AuthController: ControllerBase

{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IUserService userService, IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService=jwtTokenService;
    }
/*
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userService.UserExists(registerDto.Email))
        {
            return BadRequest("User already exist.");
            
        } 

        var user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = registerDto.FullName,
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            Role="Buyer",
            CreatedDate=DateTime.Now
        };
        await _userService.CreateUserAsync(user);

        return Ok("User registered succesfully.");
    }*/
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var user = await _userService.GetUserByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized("Invalid credintials");
        }
        if (user.LockoutEndTime.HasValue && user.LockoutEndTime > DateTime.UtcNow)
        {
            return Unauthorized("Account is locked. Please try again later.");
        }
        
        // Verifikim i password
        bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!isValidPassword)
        {
            user.FailedLoginAttempts++;
            if (user.FailedLoginAttempts >= 5)
            {
                user.LockoutEndTime = DateTime.UtcNow.AddMinutes(10); 
            }
            await _userService.UpdateUserAsync(user); 
            return Unauthorized("Invalid credentials");
        }
        // Nëse login-i është i suksesshëm, rivendos tentativat e dështuara
        user.FailedLoginAttempts = 0;
        user.LockoutEndTime = null;
        await _userService.UpdateUserAsync(user);
        
        
        var token = _jwtTokenService.GenerateToken(user);
        
        return Ok(new{Token=token,UserId = user.UserId,Role= user.Role});
    }
[HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok("Logout successful");
        // return RedirectToAction("Login","Auth");
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    



}
