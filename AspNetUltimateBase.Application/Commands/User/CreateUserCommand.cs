using MediatR;
using AspNetUltimateBase.Application.Dtos;

namespace AspNetUltimateBase.Application.Commands.User;

public class CreateUserCommand : UserDto, IRequest;
