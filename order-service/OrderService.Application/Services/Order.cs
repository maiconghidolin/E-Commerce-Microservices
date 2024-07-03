using AutoMapper;
using OrderService.Domain.Interfaces.Services;

namespace OrderService.Application.Services;

public class Order(Domain.Interfaces.Repositories.IOrder _orderRepository, IMapper _mapper) : IOrder
{
    public async Task<List<Domain.DTO.Order>> GetAll()
    {
        // it is better create a DTO for every operation (Get, Create, Update)

        var orders = await _orderRepository.GetAll();

        var mappedOrders = _mapper.Map<List<Domain.DTO.Order>>(orders);

        return mappedOrders;
    }

    public async Task<Domain.DTO.Order> Get(int id)
    {
        var order = await _orderRepository.Get(id);

        var mappedOrder = _mapper.Map<Domain.DTO.Order>(order);

        return mappedOrder;
    }

    public async Task Create(Domain.DTO.Order order)
    {
        try
        {
            var mappedOrder = _mapper.Map<Domain.Entities.Order>(order);
            mappedOrder.Status = Domain.Enums.OrderStatus.Pending;
            mappedOrder.CreatedAt = DateTime.UtcNow;
            mappedOrder.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.Create(mappedOrder);

            // send rabbitmq message for order-created
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task Update(int id, Domain.DTO.Order orderDTO)
    {
        var order = await _orderRepository.Get(id);

        if (order == null)
            throw new Exception("Order not found");

        order.UpdatedAt = DateTime.UtcNow;
        order.TotalAmount = orderDTO.TotalAmount;
        order.ShippingAddressId = orderDTO.ShippingAddressId;
        order.PaymentMethod = orderDTO.PaymentMethod;

        await _orderRepository.Update(order);
    }

    public async Task Delete(int id)
    {
        await _orderRepository.Delete(id);
    }

}
