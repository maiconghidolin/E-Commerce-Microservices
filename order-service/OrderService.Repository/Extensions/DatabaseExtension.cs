using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Repository.Extensions;

public static class DatabaseExtension
{
    public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<EFContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                x => x.MigrationsAssembly("OrderService.Repository"));
        });
    }
}
