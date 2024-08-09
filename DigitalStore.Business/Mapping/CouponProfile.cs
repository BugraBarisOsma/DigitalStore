using AutoMapper;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponRequestDTO>().ReverseMap();
        CreateMap<Coupon, CouponResponseDTO>().ReverseMap();
    }
}