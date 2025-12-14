using Microsoft.AspNetCore.Identity;
using AutoMapper;
using MediatR;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Commands.User;

public class CreateUserCommandHandler(
    IUserRepository _userRepository,
    IMapper _mapper)
    : IRequestHandler<CreateUserCommand>
{
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        UserEntity user = _mapper.Map<UserEntity>(request);

        user.RoleId = _userRepository.GetRoleIdByRoleName(request.Role);

        PasswordHasher<UserEntity> passwordHasher = new();
        user.Password = passwordHasher.HashPassword(user, request.Password);

        await _userRepository.Create(user);
        return Unit.Value;
    }
}
