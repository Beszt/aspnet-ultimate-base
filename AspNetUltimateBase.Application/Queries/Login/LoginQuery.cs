using MediatR;

namespace AspNetUltimateBase.Application.Queries.Login;

public class LoginQuery() : IRequest<string>
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string JwtKey { get; set; } = default!;
    public string JwtIssuer { get; set; } = default!;
    public int JwtExpire { get; set; }
}

