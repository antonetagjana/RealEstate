using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.models;
using WebApplication2.Services.Reservation;
using WebApplication2.Data;

namespace WebApplication2.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationService _reservationService;

        public ReservationController(ApplicationDbContext context, IReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null) return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != reservation.UserId.ToString() &&
                !User.IsInRole("Admin") &&
                !User.IsInRole("Seller"))
            {
                return Forbid();
            }

            return Ok(reservation);
        }

        [HttpGet("count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReservationCount()
        {
            var count = await _reservationService.GetReservationCountAsync();
            return Ok(count);
        }

        // [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(reservations);
        }

        [HttpPost("{userId}/{propertyId}")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateDTO reservationCreateDto, Guid userId, Guid propertyId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = new Reservation
            {
                UserId = userId,
                PropertyId = propertyId,
                CheckIn = reservationCreateDto.CheckIn,
                CheckOut = reservationCreateDto.CheckOut,
                Status = ReservationStatus.Pending
            };

            await _reservationService.AddAsync(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.ReservationId }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] ReservationUpdateDTO reservationUpdateDto, ReservationStatus newStatus)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found");
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != reservation.UserId.ToString() &&
                !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            reservation.CheckIn = reservationUpdateDto.CheckIn;
            reservation.CheckOut = reservationUpdateDto.CheckOut;
            reservation.Status = newStatus;

            await _reservationService.UpdateAsync(reservation);
            return NoContent();
        }
            // Merr numrin e rezervimeve të konfirmuara për seller-in e loguar
            /*
            [HttpGet("count-confirmed")]
            public async Task<IActionResult> GetConfirmedReservationsCount()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var count = await _context.Reservations
                    .Where(r => r.userId == userId && r.Status == "confirmed")
                    .CountAsync();
        
                return Ok(count);
            }
            */
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != reservation.UserId.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            await _reservationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
