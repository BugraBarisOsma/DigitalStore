using AutoMapper;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryRequestDTO>().ReverseMap();
        CreateMap<Category, CategoryResponseDTO>().ReverseMap();
    }
}