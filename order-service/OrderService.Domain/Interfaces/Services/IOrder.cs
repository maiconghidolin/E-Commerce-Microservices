using OrderService.Domain.DTO;

namespace OrderService.Domain.Interfaces.Services;

public interface IOrder
{
    Task<List<Order>> GetAll();

    Task<Order> Get(int id);

    Task Create(Order order);

    Task Update(int id, Order order);

    Task Delete(int id);

}
