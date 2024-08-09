using AutoMapper;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class OrderDetailProfile : Profile
{
    public OrderDetailProfile()
    {
        CreateMap<OrderItemDTO, OrderDetail>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailRequestDTO>().ReverseMap();
     
        CreateMap<OrderDetail, OrderDetailResponseDTO>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ReverseMap();
        

    }
}