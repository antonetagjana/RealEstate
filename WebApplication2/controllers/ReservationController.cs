using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.models;
using WebApplication2.Services.Reservation;

namespace WebApplication2.controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController(IReservationService reservationService) : ControllerBase
    {
        [Authorize(Policy = "MustBeAdminOrSeller")] 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            var reservation = await reservationService.GetByIdAsync(id);
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

        [Authorize(Policy = "MustBeAdminOrSeller")]
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await reservationService.GetAllAsync();
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

            await reservationService.AddAsync(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.ReservationId }, reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] ReservationUpdateDTO reservationUpdateDto, ReservationStatus newStatus)
        {
            var reservation = await reservationService.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound("Reservation not found ");
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

            await reservationService.UpdateAsync(reservation);
            return NoContent();
        }

        [Authorize] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            var reservation = await reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            if (currentUserId != reservation.UserId.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid(); 
            }

            await reservationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
