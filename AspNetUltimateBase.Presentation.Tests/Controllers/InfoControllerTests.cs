using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.Health;
using AspNetUltimateBase.Presentation.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Presentation.Tests.Controllers;

public class InfoControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenDatabaseHealthy()
    {
        Mock<IMediator> mediator = new();
        mediator.Setup(m => m.Send(It.IsAny<HealthCheckQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HealthCheckDto
            {
                Name = "App",
                Version = "1.0.0",
                CanConnectToDatabase = true
            });

        InfoController controller = new(mediator.Object);

        IActionResult result = await controller.Get();

        OkObjectResult ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(new HealthCheckDto
        {
            Name = "App",
            Version = "1.0.0",
            CanConnectToDatabase = true,
            Error = null
        });
    }

    [Fact]
    public async Task Get_ReturnsServiceUnavailable_WhenDatabaseUnavailable()
    {
        Mock<IMediator> mediator = new();
        mediator.Setup(m => m.Send(It.IsAny<HealthCheckQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HealthCheckDto
            {
                Name = "App",
                Version = "1.0.0",
                CanConnectToDatabase = false,
                Error = "fail"
            });

        InfoController controller = new(mediator.Object);

        IActionResult result = await controller.Get();

        ObjectResult obj = result.Should().BeOfType<ObjectResult>().Subject;
        obj.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        obj.Value.Should().BeEquivalentTo(new HealthCheckDto
        {
            Name = "App",
            Version = "1.0.0",
            CanConnectToDatabase = false,
            Error = "fail"
        });
    }
}
