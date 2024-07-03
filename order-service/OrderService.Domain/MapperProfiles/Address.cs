using AutoMapper;

namespace OrderService.Domain.Mappers;

public class Address : Profile
{
    public Address()
    {
        CreateMap<Entities.Address, DTO.Address>();
        CreateMap<DTO.Address, Entities.Address>();
    }
}
