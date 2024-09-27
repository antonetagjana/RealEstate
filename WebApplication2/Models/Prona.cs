
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.models;


public class Prona
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; } 
    [MaxLength (255)]
    public string Title { get; set; } = string.Empty;
    [MaxLength (255)]
    public string Description { get; set; } = string.Empty;
    [MaxLength (255)]
    public string Category { get; set; } = string.Empty; 
    [MaxLength (255)]
    public string Location { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal SurfaceArea { get; set; }
    public int Floors { get; set; }
    public bool IsPromoted { get; set; } = false;
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    
    public User User { get; set; } 
    public ICollection<PropertyPhoto> Photos { get; set; } = new List<PropertyPhoto>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}