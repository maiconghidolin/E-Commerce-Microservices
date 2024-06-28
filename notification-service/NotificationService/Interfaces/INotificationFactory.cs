namespace NotificationService.Interfaces;

public interface INotificationFactory
{
    INotification<T> Create<T>();
}
