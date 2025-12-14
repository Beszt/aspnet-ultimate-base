using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.Login;
using AspNetUltimateBase.Presentation.Controllers;
using AspNetUltimateBase.Presentation.Settings;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Presentation.Tests.Controllers;

public class LoginControllerTests
{
    private readonly Mock<IMediator> _mediator = new();
    private readonly JwtSettings _settings = new()
    {
        Key = "super-secret-key-super-secret-key",
        Issuer = "issuer",
        ExpireInDays = 1
    };

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenBodyIsNull()
    {
        LoginController controller = new(_mediator.Object, _settings);

        IActionResult result = await controller.Login(null);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Login_ReturnsOkWithToken_WhenMediatorSucceeds()
    {
        const string token = "jwt-token";
        _mediator.Setup(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(token);

        LoginController controller = new(_mediator.Object, _settings);

        IActionResult result = await controller.Login(new LoginDto { Login = "demo", Password = "pass" });

        OkObjectResult ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().Be(token);
    }
}
