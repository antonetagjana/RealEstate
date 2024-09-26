namespace WebApplication2.Repositories.Reservation;
using models;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(Guid reservationId);
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(Guid reservationId);
}