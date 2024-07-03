using AutoMapper;
using OrderService.Domain.Interfaces.Services;

namespace OrderService.Application.Services;

public class Address(Domain.Interfaces.Repositories.IAddress _addressRepository, IMapper _mapper) : IAddress
{

    public async Task<List<Domain.DTO.Address>> GetAll()
    {
        var addresses = await _addressRepository.GetAll();

        var mappedAddresses = _mapper.Map<List<Domain.DTO.Address>>(addresses);

        return mappedAddresses;
    }

    public async Task<Domain.DTO.Address> Get(int id)
    {
        var address = await _addressRepository.Get(id);

        var mappedAddress = _mapper.Map<Domain.DTO.Address>(address);

        return mappedAddress;
    }

    public async Task Create(Domain.DTO.Address address)
    {
        try
        {
            var mappedAddress = _mapper.Map<Domain.Entities.Address>(address);
            mappedAddress.CreatedAt = DateTime.UtcNow;
            mappedAddress.UpdatedAt = DateTime.UtcNow;

            await _addressRepository.Create(mappedAddress);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task Update(int id, Domain.DTO.Address addressDTO)
    {
        var address = await _addressRepository.Get(id);

        if (address == null)
            throw new Exception("Address not found");

        address.UpdatedAt = DateTime.UtcNow;
        address.Street = addressDTO.Street;
        address.City = addressDTO.City;
        address.State = addressDTO.State;
        address.ZipCode = addressDTO.ZipCode;

        await _addressRepository.Update(address);
    }

    public async Task Delete(int id)
    {
        await _addressRepository.Delete(id);
    }

}
