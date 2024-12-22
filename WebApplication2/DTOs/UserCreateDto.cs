using WebApplication2.models;

namespace WebApplication2.DTOs;

public class UserCreateDto
{
    public string hashedPassword;
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    
    
    
    
    
}