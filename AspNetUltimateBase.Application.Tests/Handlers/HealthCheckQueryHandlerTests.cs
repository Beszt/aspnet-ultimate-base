using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Interfaces;
using AspNetUltimateBase.Application.Queries.Health;
using FluentAssertions;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Application.Tests.Handlers;

public class HealthCheckQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsHealthy_WhenDatabaseAccessible()
    {
        Mock<IDatabaseHealthChecker> checker = new();
        checker.Setup(c => c.CanConnectAsync()).ReturnsAsync(true);
        HealthCheckQueryHandler handler = new(checker.Object);

        HealthCheckDto result = await handler.Handle(new HealthCheckQuery
        {
            Name = "TestApp",
            Version = "1.2.3"
        }, CancellationToken.None);

        result.Name.Should().Be("TestApp");
        result.Version.Should().Be("1.2.3");
        result.CanConnectToDatabase.Should().BeTrue();
        result.Error.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_ReturnsUnhealthy_WhenDatabaseUnavailable()
    {
        Mock<IDatabaseHealthChecker> checker = new();
        checker.Setup(c => c.CanConnectAsync()).ReturnsAsync(false);
        HealthCheckQueryHandler handler = new(checker.Object);

        HealthCheckDto result = await handler.Handle(new HealthCheckQuery
        {
            Name = "TestApp",
            Version = "1.2.3"
        }, CancellationToken.None);

        result.CanConnectToDatabase.Should().BeFalse();
        result.Error.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_ReturnsUnhealthyWithError_WhenExceptionThrown()
    {
        Mock<IDatabaseHealthChecker> checker = new();
        checker.Setup(c => c.CanConnectAsync()).ThrowsAsync(new InvalidOperationException("boom"));
        HealthCheckQueryHandler handler = new(checker.Object);

        HealthCheckDto result = await handler.Handle(new HealthCheckQuery
        {
            Name = "TestApp",
            Version = "1.2.3"
        }, CancellationToken.None);

        result.CanConnectToDatabase.Should().BeFalse();
        result.Error.Should().Be("boom");
    }
}
