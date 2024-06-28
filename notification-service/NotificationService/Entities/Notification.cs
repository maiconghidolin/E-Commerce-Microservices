using MongoDB.Bson.Serialization.Attributes;

namespace NotificationService.Entities;

public class Notification
{
    [BsonId]
    public string Id { get; set; }

    public string Type { get; set; }

    public string UserId { get; set; }

    public string EmailAdress { get; set; }

    public string DeviceId { get; set; }

    public string Number { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public DateTime CreatedAt { get; set; }
}
