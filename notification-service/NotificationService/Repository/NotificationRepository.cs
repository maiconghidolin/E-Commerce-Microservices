using MongoDB.Driver;
using NotificationService.Entities;
using NotificationService.Interfaces;

namespace NotificationService.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notification> _notificationCollection;

    public NotificationRepository(IMongoDatabase mongoDatabase)
    {
        _notificationCollection = mongoDatabase.GetCollection<Notification>("Notifications");
    }

    public async Task<Notification> CreateAsync(Notification model)
    {
        model.Id = Guid.NewGuid().ToString();
        model.CreatedAt = DateTime.Now;

        await _notificationCollection.InsertOneAsync(model);
        return model;
    }

    public async Task DeleteAsync(string id)
    {
        await _notificationCollection.DeleteOneAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Notification>> GetAllAsync(int offset, int fetch)
    {
        var filter = Builders<Notification>.Filter.Empty;

        return _notificationCollection
            .Find(filter)
            .Skip(offset)
            .Limit(fetch)
            .ToList();
    }

    public async Task<Notification> GetByIdAsync(string id)
    {
        return await _notificationCollection.Find(model => model.Id == id).FirstOrDefaultAsync();
    }

}
