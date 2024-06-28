namespace NotificationService.DTO.Notification;

public class Push
{
    public string UserId { get; set; }
    public string DeviceId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
