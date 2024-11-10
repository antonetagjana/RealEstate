using Microsoft.AspNetCore.Mvc;
using WebApplication2.models;
using WebApplication2.Services.Notification;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(Guid id)
    {
        var notification = await notificationService.GetByIdAsync(id);
        if (notification == null) return NotFound();
        return Ok(notification);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications = await notificationService.GetAllAsync();
        return Ok(notifications);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification(Notification notification)
    {
        await notificationService.AddAsync(notification);
        return CreatedAtAction(nameof(GetNotificationById), new { id = notification.NotificationId }, notification);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(Guid id, Notification notification)
    {
        if (id != notification.NotificationId) return BadRequest();

        await notificationService.UpdateAsync(notification);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        await notificationService.DeleteAsync(id);
        return NoContent();
    }
}
