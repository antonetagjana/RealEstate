namespace WebApplication2.Services.Reservation;
using models;

public interface IReservationService
{
    Task<Reservation?> GetByIdAsync(Guid reservationId);
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(Guid reservationId);
}