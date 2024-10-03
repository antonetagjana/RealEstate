namespace WebApplication2.models;

using System.Text.Json.Serialization;


public class Reservation
{
    public Guid ReservationId { get; set; }
    
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus Status { get; set; }
    

    public Prona Property { get; set; }
    public Guid PropertyId { get; set; } 
    
    public User User { get; set; }
    public Guid UserId { get; set; }

   
}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReservationStatus
{
    Pending,
    Approved,
    Declined,
}
