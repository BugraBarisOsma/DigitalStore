using AutoMapper;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductRequestDTO>().ReverseMap();
        CreateMap<Product, ProductResponseDTO>().ReverseMap();
    }
}