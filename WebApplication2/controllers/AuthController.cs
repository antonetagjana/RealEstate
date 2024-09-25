using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly EmailService _emailService;
    
    public AuthController(IConfiguration configuration,EmailService emailService)
    {
        _configuration = configuration;
        _emailService = emailService;
    }

    // Endpoint për Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        // Verifikimi i thjeshtë i kredencialeve (ky duhet zëvendësuar me një bazë të dhënash)
        if (loginModel.Username == "test" && loginModel.Password == "password")  // Ky është një shembull, ndrysho për të verifikuar nga DB
        {
            // Nëse kredencialet janë të sakta, gjenero JWT token
            var token = GenerateJwtToken(loginModel.Username);
            
            // Dërgo email për përdoruesin e ri
            await _emailService.SendEmailAsync(loginModel.Email, "Konfirmimi i Llogarisë", 
                $"Përshëndetje {loginModel.Username}, llogaria juaj u krijua me sukses!");


            // Kthe token-in te klienti
            return Ok(new { token });
        }

        return Unauthorized();  // Nëse kredencialet nuk janë të sakta
    }

    // Funksion për gjenerimin e JWT token
    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);  // Merr çelësin sekret nga konfigurimi

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)  // Krijo një claim për emrin e përdoruesit
            }),
            Expires = DateTime.UtcNow.AddHours(1),  // Token do të skadojë pas 1 ore
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  // Nënshkruaj token-in
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);  // Kthe token-in si string
    }
}