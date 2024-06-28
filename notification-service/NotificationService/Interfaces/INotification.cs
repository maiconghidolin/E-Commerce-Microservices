namespace NotificationService.Interfaces;

public interface INotification<T>
{
    Task<string> Send(T data);
}
