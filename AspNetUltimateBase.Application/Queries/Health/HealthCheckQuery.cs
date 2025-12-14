using AspNetUltimateBase.Application.Dtos;
using MediatR;

namespace AspNetUltimateBase.Application.Queries.Health;

public class HealthCheckQuery : IRequest<HealthCheckDto>
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}
