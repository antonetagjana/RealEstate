using WebApplication2.Repositories.Notification;

namespace WebApplication2.Services.Notification;
using models;

public class NotificationService(INotificationRepository notificationRepository) : INotificationService
{
    public Task<Notification?> GetByIdAsync(Guid notificationId)
    {
        return notificationRepository.GetByIdAsync(notificationId);
    }

    public Task<IEnumerable<Notification>> GetAllAsync()
    {
        return notificationRepository.GetAllAsync();
    }

    public Task AddAsync(Notification notification)
    {
        return notificationRepository.AddAsync(notification);
    }

    public Task UpdateAsync(Notification notification)
    {
        return notificationRepository.UpdateAsync(notification);
    }

    public Task DeleteAsync(Guid notificationId)
    {
        return notificationRepository.DeleteAsync(notificationId);
    }
}