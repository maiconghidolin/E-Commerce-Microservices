using AutoMapper;

namespace OrderService.Domain.Mappers;

public class Order : Profile
{
    public Order()
    {
        CreateMap<Entities.Order, DTO.Order>();
        CreateMap<DTO.Order, Entities.Order>();
    }
}
