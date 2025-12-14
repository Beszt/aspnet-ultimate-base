using AutoMapper;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserDto>();

        CreateMap<UserDto, UserEntity>()
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}
