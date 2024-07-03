using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Services;
using OrderService.Domain.Interfaces.Services;

namespace OrderService.Application.Extensions;

public static class ServiceInjection
{
    public static void AddServiceInjection(this IServiceCollection services)
    {
        services.AddTransient<IOrder, Order>();
        services.AddTransient<IOrderItem, OrderItem>();
        services.AddTransient<IAddress, Address>();
    }
}
