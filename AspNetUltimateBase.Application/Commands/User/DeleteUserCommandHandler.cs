using MediatR;
using AspNetUltimateBase.Domain.Interfaces;
namespace AspNetUltimateBase.Application.Commands.User;

public class DeleteUserCommandHandler(
    IUserRepository _userRepository)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.Delete(request.Login);
        return Unit.Value;
    }
}
