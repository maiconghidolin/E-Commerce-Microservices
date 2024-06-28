using NotificationService.Interfaces;

namespace NotificationService.EventListeners.UserCreated;

public class UserCreatedEventListenerHostedService : BackgroundService
{
    private readonly IEventListener _eventListener;

    public UserCreatedEventListenerHostedService(IEventListener eventListener)
    {
        _eventListener = eventListener;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _eventListener.Init();
    }
}
