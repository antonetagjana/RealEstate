using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.models;
using WebApplication2.Data;
using WebApplication2.Repositories.Reservation;

namespace WebApplication2.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ApplicationDbContext _context;

        public ReservationService(IReservationRepository reservationRepository, ApplicationDbContext context)
        {
            _reservationRepository = reservationRepository;
            _context = context;
        }

        public Task<models.Reservation?> GetByIdAsync(Guid reservationId)
        {
            return _reservationRepository.GetByIdAsync(reservationId);
        }

        public Task<IEnumerable<models.Reservation>> GetAllAsync()
        {
            return _reservationRepository.GetAllAsync();
        }

        public async Task<int> GetReservationCountAsync()
        {
            return await _context.Reservations.CountAsync();
        }

        public Task AddAsync(models.Reservation reservation)
        {
            return _reservationRepository.AddAsync(reservation);
        }

        public Task UpdateAsync(models.Reservation reservation)
        {
            return _reservationRepository.UpdateAsync(reservation);
        }

        public Task DeleteAsync(Guid reservationId)
        {
            return _reservationRepository.DeleteAsync(reservationId);
        }
    }
}
