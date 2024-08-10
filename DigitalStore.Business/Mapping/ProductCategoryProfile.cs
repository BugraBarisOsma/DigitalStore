using AutoMapper;
using DigitalStore.Core.Domains;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class ProductCategoryProfile : Profile
{
  public ProductCategoryProfile()
  {
    CreateMap<ProductCategory, ProductCategoryRequestDTO>().ReverseMap();
    CreateMap<ProductCategory, ProductCategoryResponseDTO>()
      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));}
  
}