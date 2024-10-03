using WebApplication2.models;

namespace WebApplication2.DTOs;

public class ReservationUpdateDTO
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public ReservationStatus Status { get; set; } 
}