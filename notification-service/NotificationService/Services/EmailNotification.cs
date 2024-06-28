using NotificationService.Interfaces;

namespace NotificationService.Services;

public class EmailNotification(INotificationRepository notificationRepository) : INotification<DTO.Notification.Email>
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;

    public async Task<string> Send(DTO.Notification.Email data)
    {
        await Task.Delay(3000);

        var notification = await _notificationRepository.CreateAsync(new Entities.Notification()
        {
            Type = "Email",
            EmailAdress = data.EmailAdress,
            Subject = data.Subject,
            Body = data.Body
        });

        return $"Notification {notification.Id} sended by e-mail to {data.EmailAdress}";
    }

}