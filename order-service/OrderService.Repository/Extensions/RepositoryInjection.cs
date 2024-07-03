using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Interfaces.Repositories;
using OrderService.Repository.Repositories;

namespace OrderService.Repository.Extensions;

public static class RepositoryInjection
{
    public static void AddRepositoryInjection(this IServiceCollection services)
    {
        services.AddTransient<IOrder, Order>();
        services.AddTransient<IAddress, Address>();
    }
}