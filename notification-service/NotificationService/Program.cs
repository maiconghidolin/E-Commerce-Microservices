using NotificationService.DTO.Notification;
using NotificationService.EventListeners.UserCreated;
using NotificationService.Extensions;
using NotificationService.Interfaces;
using NotificationService.Repository;
using NotificationService.Services;
using NotificationService.UseCases;

namespace NotificationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddScoped<PaymentService>();
        builder.Services.AddSingleton<IEventListener>(sp => new UserCreatedEventListener(builder.Configuration, sp));
        builder.Services.AddHostedService<UserCreatedEventListenerHostedService>();

        builder.Services.AddScoped<SendUserCreatedNotification>();

        builder.Services.AddMongoDatabase(builder.Configuration);
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

        builder.Services.AddScoped<INotification<Email>, EmailNotification>();
        builder.Services.AddScoped<INotification<SMS>, SMSNotification>();
        builder.Services.AddScoped<INotification<Push>, PushNotification>();

        builder.Services.AddScoped<INotificationFactory, NotificationFactory>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
