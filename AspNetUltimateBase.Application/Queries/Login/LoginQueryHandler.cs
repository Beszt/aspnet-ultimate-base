using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using AspNetUltimateBase.Domain.Interfaces;
using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Application.Queries.Login;

public class LoginQueryHandler(
    IUserRepository _userRepository)
    : IRequestHandler<LoginQuery, string>
{
    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        UserEntity user = await _userRepository.Get(request.Login);

        if (user == null)
            return null;

        PasswordHasher<UserEntity> passwordHasher = new();

        if (passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            return null;

        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role.Name)
        ];

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.JwtKey));
        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.Now.AddDays(request.JwtExpire);

        JwtSecurityToken token = new JwtSecurityToken(
            request.JwtIssuer,
            request.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}

