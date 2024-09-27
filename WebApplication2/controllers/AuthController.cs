using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.Services.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

/*
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
    }
}*/