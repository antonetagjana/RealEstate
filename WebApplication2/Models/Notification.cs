using System.ComponentModel.DataAnnotations;

namespace WebApplication2.models;

public class Notification
{
    public Guid NotificationId { get; set; } 
    public Guid UserId { get; set; } 
    [MaxLength(255)]
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public User User { get; set; }
}
