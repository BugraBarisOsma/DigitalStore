using AutoMapper;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderRequestDTO>().ReverseMap();

        CreateMap<Order, OrderDetailResponseDTO>().ReverseMap();
        CreateMap<Order, OrderResponseDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)) 
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)) 
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails)) 
            .ReverseMap();
     
    }
}