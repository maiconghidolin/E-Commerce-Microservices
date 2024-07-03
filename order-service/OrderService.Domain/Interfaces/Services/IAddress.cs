using OrderService.Domain.DTO;

namespace OrderService.Domain.Interfaces.Services;

public interface IAddress
{
    Task<List<Address>> GetAll();

    Task<Address> Get(int id);

    Task Create(Address address);

    Task Update(int id, Address address);

    Task Delete(int id);
}
