using OrderService.Application.Extensions;
using OrderService.Presentation.Extensions;
using OrderService.Repository.Extensions;
using System.Reflection;

namespace OrderService.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure();

        var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

        builder.Services.ConfigureDatabase(connectionString);

        builder.Services.AddServiceInjection();

        builder.Services.AddRepositoryInjection();

        builder.Services.AddAutoMapper(Assembly.Load("OrderService.Domain"));

        var app = builder.Build();

        app.Configure();

        app.Run();
    }
}
