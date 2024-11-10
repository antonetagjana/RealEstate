using System.ComponentModel.DataAnnotations;

namespace WebApplication2.models;

public class PropertyPhoto
{
    public Guid PhotoId { get; set; }
    public Guid PropertyId { get; set; } // Foreign key to Property
    [MaxLength(255)]
    public string PhotoUrl { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Navigation properties
    public Prona Property { get; set; } // Navigation back to Property
}