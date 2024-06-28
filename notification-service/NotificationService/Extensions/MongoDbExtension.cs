using MongoDB.Driver;

namespace NotificationService.Extensions;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDb"));
        var mongoDatabase = mongoClient.GetDatabase("NotificationsDB");
        services.AddScoped(provider => mongoDatabase);
        return services;
    }
}