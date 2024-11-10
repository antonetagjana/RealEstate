namespace WebApplication2.models;

public class Reservation
{
    public Guid ReservationId { get; set; }
    public Guid PropertyId { get; set; } // Foreign key to Property
    public Guid BuyerId { get; set; } // Foreign key to UserTable (Buyer)
    public DateTime ReservationDate { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected, Completed
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    // Navigation properties
    public Prona Property { get; set; }
    public User Buyer { get; set; }
}
