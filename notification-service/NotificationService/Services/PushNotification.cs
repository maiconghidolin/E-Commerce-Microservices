using NotificationService.Interfaces;
namespace NotificationService.Services;

public class PushNotification(INotificationRepository notificationRepository) : INotification<DTO.Notification.Push>
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;

    public async Task<string> Send(DTO.Notification.Push data)
    {
        await Task.Delay(3000);

        var notification = await _notificationRepository.CreateAsync(new Entities.Notification()
        {
            Type = "Push",
            DeviceId = data.DeviceId,
            Subject = data.Title,
            Body = data.Body
        });

        return $"Notification {notification.Id} sended by push to {data.DeviceId}";
    }
}