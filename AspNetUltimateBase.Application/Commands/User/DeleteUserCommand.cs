using MediatR;

namespace AspNetUltimateBase.Application.Commands.User;

public class DeleteUserCommand() : IRequest
{
    public string Login { get; set; } = default!;
}
