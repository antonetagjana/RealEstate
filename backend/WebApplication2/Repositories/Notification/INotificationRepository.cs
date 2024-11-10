namespace WebApplication2.Repositories.Notification;
using models;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid notificationId);
    Task<IEnumerable<Notification>> GetAllAsync();
    Task AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task DeleteAsync(Guid notificationId);  
}