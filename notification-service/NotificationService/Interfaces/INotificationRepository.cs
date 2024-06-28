using NotificationService.Entities;

namespace NotificationService.Interfaces;

public interface INotificationRepository
{
    Task<Notification> CreateAsync(Notification model);
    Task DeleteAsync(string id);
    Task<Notification> GetByIdAsync(string id);
    Task<IEnumerable<Notification>> GetAllAsync(int offset, int fetch);
}
