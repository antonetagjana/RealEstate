
namespace WebApplication2.Services;

public interface IJwtTokenService
{
    string GenerateToken(models.User user);
    
}