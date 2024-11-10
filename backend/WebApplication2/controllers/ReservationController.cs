using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.Services.Reservation;

namespace WebApplication2.controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationService reservationService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        var reservation = await reservationService.GetByIdAsync(id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await reservationService.GetAllAsync();
        return Ok(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await reservationService.AddAsync(reservation);
        return CreatedAtAction(nameof(GetReservationById), new { id = reservation.ReservationId }, reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(Guid id, [FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != reservation.ReservationId)
            return BadRequest("Reservation ID mismatch");

        await reservationService.UpdateAsync(reservation);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        var reservation = await reservationService.GetByIdAsync(id);
        if (reservation == null)
            return NotFound();

        await reservationService.DeleteAsync(id);
        return NoContent();
    }
}