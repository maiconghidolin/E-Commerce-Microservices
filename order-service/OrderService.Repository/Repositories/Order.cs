using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Interfaces.Repositories;

namespace OrderService.Repository.Repositories;

public class Order(EFContext _context) : IOrder
{

    public async Task<Domain.Entities.Order> Get(int id)
    {
        return await _context.Orders
                        .FindAsync(id);
    }

    public async Task<List<Domain.Entities.Order>> GetAll()
    {
        // need to do the pagination 

        return await _context.Orders
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task Create(Domain.Entities.Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Domain.Entities.Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        await _context.Orders.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

}
