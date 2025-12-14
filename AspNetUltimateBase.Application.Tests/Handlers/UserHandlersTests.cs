using AutoMapper;
using AspNetUltimateBase.Application.Commands.User;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Mappings;
using AspNetUltimateBase.Application.Queries.User;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Application.Tests.Handlers;

public class UserHandlersTests
{
    private readonly IMapper _mapper;

    public UserHandlersTests()
    {
        MapperConfiguration config = new(cfg => cfg.AddProfile<UserMappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task UpdateUserCommandHandler_MapsRoleAndHashesPassword()
    {
        Mock<IUserRepository> repository = new();
        repository.Setup(r => r.GetRoleIdByRoleName("admin")).Returns(3);

        UserEntity? capturedUser = null;
        repository.Setup(r => r.Update(It.IsAny<UserEntity>()))
            .Callback<UserEntity>(u => capturedUser = u)
            .Returns(Task.CompletedTask);

        IRequestHandler<UpdateUserCommand> handler = new UpdateUserCommandHandler(repository.Object, _mapper);

        UpdateUserCommand command = new()
        {
            Login = "alice",
            Password = "s3cr3t",
            Role = "admin"
        };

        await handler.Handle(command, CancellationToken.None);

        repository.Verify(r => r.Update(It.IsAny<UserEntity>()), Times.Once);
        capturedUser.Should().NotBeNull();
        capturedUser!.Login.Should().Be(command.Login);
        capturedUser.RoleId.Should().Be(3);

        PasswordHasher<UserEntity> hasher = new();
        hasher.VerifyHashedPassword(capturedUser, capturedUser.Password, command.Password)
            .Should().Be(PasswordVerificationResult.Success);
    }

    [Fact]
    public async Task GetUserQueryHandler_HidesPasswordAndAddsRoleName()
    {
        Mock<IUserRepository> repository = new();

        UserEntity entity = new()
        {
            Login = "bob",
            Password = "stored",
            RoleId = 1,
            Role = new RoleEntity { Name = "user" }
        };

        repository.Setup(r => r.Get(entity.Login)).ReturnsAsync(entity);
        repository.Setup(r => r.GetRoleName(entity.Login)).Returns("user");

        IRequestHandler<GetUserQuery, UserDto> handler = new GetUserQueryHandler(repository.Object, _mapper);

        UserDto? result = await handler.Handle(new GetUserQuery(entity.Login), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Login.Should().Be(entity.Login);
        result.Password.Should().Be("HIDDEN");
        result.Role.Should().Be("user");
    }
}
