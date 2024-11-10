using WebApplication2.Repositories.Reservation;

namespace WebApplication2.Services.Reservation;

using WebApplication2.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class ReservationService(IReservationRepository reservationRepository) : IReservationService
{
    public Task<Reservation?> GetByIdAsync(Guid reservationId)
    {
        return reservationRepository.GetByIdAsync(reservationId);
    }

    public Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return reservationRepository.GetAllAsync();
    }

    public Task AddAsync(Reservation reservation)
    {
        return reservationRepository.AddAsync(reservation);
    }

    public Task UpdateAsync(Reservation reservation)
    {
        return reservationRepository.UpdateAsync(reservation);
    }

    public Task DeleteAsync(Guid reservationId)
    {
        return reservationRepository.DeleteAsync(reservationId);
    }
}
