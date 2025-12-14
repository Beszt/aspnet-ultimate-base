using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.User;
using AspNetUltimateBase.Presentation.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Presentation.Tests.Controllers;

public class UserControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenUserDoesNotExist()
    {
        ServiceProvider services = new ServiceCollection().BuildServiceProvider();
        Mock<IMediator> mediator = new();
        mediator.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserDto?)null);

        UserController controller = new(services, mediator.Object);

        IActionResult result = await controller.Get("ghost");

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Get_ReturnsOkWithUser_WhenUserExists()
    {
        ServiceProvider services = new ServiceCollection().BuildServiceProvider();
        Mock<IMediator> mediator = new();
        mediator.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserDto { Login = "bob", Role = "user", Password = "HIDDEN" });

        UserController controller = new(services, mediator.Object);

        IActionResult result = await controller.Get("bob");

        OkObjectResult ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeOfType<UserDto>();
    }
}
