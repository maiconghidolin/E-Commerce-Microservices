using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces.Repositories;

public interface IOrder
{
    Task<List<Order>> GetAll();

    Task<Order> Get(int id);

    Task Create(Order order);

    Task Update(Order order);

    Task Delete(int id);
}
