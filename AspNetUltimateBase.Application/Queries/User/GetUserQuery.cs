using AspNetUltimateBase.Application.Dtos;
using MediatR;

namespace AspNetUltimateBase.Application.Queries.User;

public class GetUserQuery(string _Login) : IRequest<UserDto>
{
    public string Login { get; set; } = _Login;
}

