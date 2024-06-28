using NotificationService.Interfaces;
namespace NotificationService.Services;

public class SMSNotification(INotificationRepository notificationRepository) : INotification<DTO.Notification.SMS>
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;

    public async Task<string> Send(DTO.Notification.SMS data)
    {
        await Task.Delay(3000);

        var notification = await _notificationRepository.CreateAsync(new Entities.Notification()
        {
            Type = "SMS",
            Number = data.Number,
            Subject = data.Subject,
            Body = data.Body
        });

        return $"Notification {notification.Id} sended by SMS to {data.Number}";
    }
}
