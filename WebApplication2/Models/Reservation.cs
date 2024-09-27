namespace WebApplication2.models;

public class Reservation
{
    public Guid ReservationId { get; set; }
    public Guid PropertyId { get; set; } 
    public Guid BuyerId { get; set; } 
    public DateTime ReservationDate { get; set; }
    public string Status { get; set; } = "Pending"; 
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public Prona Property { get; set; }
    public User Buyer { get; set; }
}
