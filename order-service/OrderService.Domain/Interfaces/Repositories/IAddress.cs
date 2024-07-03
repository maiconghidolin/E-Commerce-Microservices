using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces.Repositories;

public interface IAddress
{
    Task<List<Address>> GetAll();

    Task<Address> Get(int id);

    Task Create(Address address);

    Task Update(Address address);

    Task Delete(int id);
}
