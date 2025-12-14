using MediatR;
using AspNetUltimateBase.Domain.Interfaces;
namespace AspNetUltimateBase.Application.Commands.User;

public class DeleteUserCommandHandler(
    IUserRepository _userRepository)
    : IRequestHandler<DeleteUserCommand>
{
    async Task IRequestHandler<DeleteUserCommand>.Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.Login);
    }
}
