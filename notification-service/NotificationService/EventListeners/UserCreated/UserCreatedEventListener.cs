using NotificationService.Interfaces;
using NotificationService.UseCases;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotificationService.EventListeners.UserCreated;

public class UserCreatedEventListener : IEventListener, IDisposable
{
    private readonly ConfigurationManager _configuration;
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IModel _channel;
    private const string _exchangeName = "user-exchange";
    private const string _queueName = "user-created-notification";

    public UserCreatedEventListener(ConfigurationManager configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public async Task Init()
    {
        try
        {
            var hostname = _configuration["RabbitMQ:Host"];
            var username = _configuration["RabbitMQ:Username"];
            var password = _configuration["RabbitMQ:Password"];

            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchangeName, type: "topic", durable: true);
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: "user.created");

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var user = JsonSerializer.Deserialize<DTO.UserCreated>(message, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                using var scope = _serviceProvider.CreateScope();

                var userCreatedNotificationUseCase = scope.ServiceProvider.GetRequiredService<SendUserCreatedNotification>();

                await userCreatedNotificationUseCase.Execute(user);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
            _channel.Close();

        if (_connection.IsOpen)
            _connection.Close();

        _connection = null;
        _channel = null;
    }
}
