using System.ComponentModel.DataAnnotations;

namespace WebApplication2.models;

public class User
{
    public Guid UserId { get; set; }
    [MaxLength(255)]
    public string FullName { get; set; } = string.Empty;
   [Required,EmailAddress] 
   public string Email { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Password { get; set; } = string.Empty; 
   [MaxLength(50)]
    public string Role { get; set; } = "Buyer"; 
   [MaxLength(50)]
    public string? PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<Prona> Properties { get; set; } = new List<Prona>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
    public int FailedLoginAttempts { get; set; } = 0; // Tentativat e dështuara
    public DateTime? LockoutEndTime { get; set; }  // Koha deri kur është i bllokuar

}
