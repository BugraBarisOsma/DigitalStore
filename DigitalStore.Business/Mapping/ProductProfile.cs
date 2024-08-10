using AutoMapper;
using DigitalStore.Core.Domains;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductRequestDTO>().ReverseMap();
        CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.ProductCategories, opt => opt.MapFrom(src => src.ProductCategories ?? new List<ProductCategory>()));

    }
}