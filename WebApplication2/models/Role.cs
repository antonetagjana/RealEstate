namespace WebApplication2.models;

public class Role
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;

    // Navigation property to Users
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
