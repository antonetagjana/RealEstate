namespace WebApplication2.DTOs;

public class PropertyFilterDTO
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Category { get; set; } // Në vend të 'PropertyType'
    public string? Location { get; set; }
    public int? Floors { get; set; } // Në vend të 'Rooms'

}