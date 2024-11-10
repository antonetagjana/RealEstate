using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Repositories.Reservation;
using models;

public class ReservationRepository(ApplicationDbContext dbContext) : IReservationRepository
{
    public async Task<Reservation?> GetByIdAsync(Guid reservationId)
    {
        return await dbContext.Reservations.Include(r => r.Property)
            .Include(r => r.Buyer)
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await dbContext.Reservations.ToListAsync();
    }

    public async Task AddAsync(Reservation reservation)
    {
        await dbContext.Reservations.AddAsync(reservation);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        dbContext.Reservations.Update(reservation);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid reservationId)
    {
        var reservation = await GetByIdAsync(reservationId);
        if (reservation != null)
        {
            dbContext.Reservations.Remove(reservation);
            await dbContext.SaveChangesAsync();
        }
    }
}