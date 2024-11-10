using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;


namespace WebApplication2.Repositories.Notification;
using models;

public class NotificationRepository(ApplicationDbContext dbContext) : INotificationRepository
{
    public async Task<Notification?> GetByIdAsync(Guid notificationId)
    {
        return await dbContext.Notifications.Include(n => n.User)
            .FirstOrDefaultAsync(n => n.NotificationId == notificationId);
    }

    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await dbContext.Notifications.ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await dbContext.Notifications.AddAsync(notification);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Notification notification)
    {
        dbContext.Notifications.Update(notification);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid notificationId)
    {
        var notification = await GetByIdAsync(notificationId);
        if (notification != null)
        {
            dbContext.Notifications.Remove(notification);
            await dbContext.SaveChangesAsync();
        }
    }
}