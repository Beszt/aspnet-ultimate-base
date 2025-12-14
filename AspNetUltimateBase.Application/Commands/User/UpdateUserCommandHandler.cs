using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MediatR;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Commands.User;

public class UpdateUserCommandHandler(
    IUserRepository _userRepository,
    IMapper _mapper)
    : IRequestHandler<UpdateUserCommand>
{
    async Task IRequestHandler<UpdateUserCommand>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        UserEntity user = _mapper.Map<UserEntity>(request);

        user.RoleId = _userRepository.GetRoleIdByRoleName(request.Role);

        PasswordHasher<UserEntity> passwordHasher = new();
        user.Password = passwordHasher.HashPassword(user, request.Password);

        await _userRepository.Update(user);
    }
}

