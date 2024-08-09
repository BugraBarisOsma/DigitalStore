using AutoMapper;
using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRequestDTO>().ReverseMap();
        CreateMap<User, UserResponseDTO>().ReverseMap();
    }
}