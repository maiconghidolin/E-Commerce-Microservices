using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Interfaces.Repositories;

namespace OrderService.Repository.Repositories;

public class Address(EFContext _context) : IAddress
{

    public async Task<Domain.Entities.Address> Get(int id)
    {
        return await _context.Addresses
                        .FindAsync(id);
    }

    public async Task<List<Domain.Entities.Address>> GetAll()
    {
        // need to do the pagination 

        return await _context.Addresses
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task Create(Domain.Entities.Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Domain.Entities.Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        await _context.Addresses.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

}
