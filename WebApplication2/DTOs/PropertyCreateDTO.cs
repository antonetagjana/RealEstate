namespace WebApplication2.DTOs;

public class PropertyCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public decimal Price { get; set; }
    public decimal SurfaceArea { get; set; }
    public int Floors { get; set; }
    public bool IsPromoted { get; set; } = false;
    public bool IsAvailable { get; set; } = true;
    
}
