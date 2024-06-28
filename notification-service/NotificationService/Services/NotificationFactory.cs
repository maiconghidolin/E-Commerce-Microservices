using NotificationService.DTO.Notification;
using NotificationService.Interfaces;

namespace NotificationService.Services;

public class NotificationFactory : INotificationFactory
{
    private readonly Dictionary<Type, Func<object>> _factories;

    public NotificationFactory(IServiceProvider serviceProvider)
    {
        _factories = new Dictionary<Type, Func<object>>
        {
            { typeof(Email), () => serviceProvider.GetService<INotification<Email>>() },
            { typeof(SMS), () => serviceProvider.GetService<INotification<SMS>>() },
            { typeof(PushNotification), () => serviceProvider.GetService<INotification<PushNotification>>() }
        };
    }

    public INotification<T> Create<T>()
    {
        if (_factories.TryGetValue(typeof(T), out var factory))
            return factory() as INotification<T>;

        throw new NotSupportedException($"Notification type {typeof(T).Name} is not supported.");
    }
}
