using NotificationService.DTO;
using NotificationService.DTO.Notification;
using NotificationService.Interfaces;

namespace NotificationService.UseCases;

public class SendUserCreatedNotification(INotificationFactory notificationFactory)
{
    public async Task Execute(UserCreated user)
    {
        List<Task<string>> tasks = new();

        Email email = new()
        {
            UserId = user.Id,
            EmailAdress = user.Email,
            Subject = "Welcome to our platform",
            Body = "You have successfully created an account."
        };

        var taskEmail = notificationFactory.Create<Email>().Send(email);
        tasks.Add(taskEmail);

        if (!string.IsNullOrEmpty(user.Number))
        {
            SMS sms = new()
            {
                UserId = user.Id,
                Number = user.Number,
                Subject = "Welcome to our platform",
                Body = "You have successfully created an account."
            };

            var taskSMS = notificationFactory.Create<SMS>().Send(sms);
            tasks.Add(taskSMS);
        }

        if (!string.IsNullOrEmpty(user.DeviceId))
        {
            Push push = new()
            {
                UserId = user.Id,
                DeviceId = user.DeviceId,
                Title = "Welcome to our platform",
                Body = "You have successfully created an account."
            };

            var taskPush = notificationFactory.Create<Push>().Send(push);
            tasks.Add(taskPush);
        }

        await Task.WhenAll(tasks);
    }
}
