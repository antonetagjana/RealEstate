namespace WebApplication2.models;

public class UserRole
{
    public Guid UserId { get; set; }
    public UserTable User { get; set; } = null!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
