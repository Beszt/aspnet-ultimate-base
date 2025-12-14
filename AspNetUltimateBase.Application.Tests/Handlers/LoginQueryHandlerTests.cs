using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AspNetUltimateBase.Application.Queries.Login;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Application.Tests.Handlers;

public class LoginQueryHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();

    [Fact]
    public async Task Handle_ReturnsTokenWithClaims_WhenCredentialsAreValid()
    {
        UserEntity user = new()
        {
            Id = 42,
            Login = "demo",
            Role = new RoleEntity { Name = "admin" }
        };

        PasswordHasher<UserEntity> passwordHasher = new();
        user.Password = passwordHasher.HashPassword(user, "p@ssword");

        _repository.Setup(r => r.Get(user.Login)).ReturnsAsync(user);

        LoginQueryHandler handler = new(_repository.Object);

        string token = await handler.Handle(new LoginQuery
        {
            Login = user.Login,
            Password = "p@ssword",
            JwtKey = "super-secret-key-super-secret-key",
            JwtIssuer = "issuer",
            JwtExpire = 1
        }, CancellationToken.None);

        token.Should().NotBeNullOrWhiteSpace();

        JwtSecurityToken parsed = new JwtSecurityTokenHandler().ReadJwtToken(token);
        parsed.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
        parsed.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == user.Login);
        parsed.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == user.Role.Name);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenPasswordIsInvalid()
    {
        UserEntity user = new()
        {
            Login = "demo",
            Role = new RoleEntity { Name = "user" }
        };

        PasswordHasher<UserEntity> passwordHasher = new();
        user.Password = passwordHasher.HashPassword(user, "p@ssword");

        _repository.Setup(r => r.Get(user.Login)).ReturnsAsync(user);

        LoginQueryHandler handler = new(_repository.Object);

        string? token = await handler.Handle(new LoginQuery
        {
            Login = user.Login,
            Password = "wrong",
            JwtKey = "super-secret-key-super-secret-key",
            JwtIssuer = "issuer",
            JwtExpire = 1
        }, CancellationToken.None);

        token.Should().BeNull();
    }
}
