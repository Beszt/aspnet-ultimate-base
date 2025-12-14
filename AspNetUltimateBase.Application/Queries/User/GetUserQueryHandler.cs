using AutoMapper;
using MediatR;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Queries.User;

public class GetUserQueryHandler(
    IUserRepository _userRepository,
    IMapper _mapper)
    : IRequestHandler<GetUserQuery, UserDto>
{
    async Task<UserDto> IRequestHandler<GetUserQuery, UserDto>.Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        UserEntity user = await _userRepository.Get(request.Login);
        UserDto dto = _mapper.Map<UserDto>(user);

        if (dto != null)
        {
            dto.Password = "HIDDEN";
            dto.Role = _userRepository.GetRoleName(dto.Login)!;
        }

        return dto;
    }
}

