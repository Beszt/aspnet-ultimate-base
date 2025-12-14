using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Interfaces;
using MediatR;

namespace AspNetUltimateBase.Application.Queries.Health;

public class HealthCheckQueryHandler(IDatabaseHealthChecker _databaseHealthChecker)
    : IRequestHandler<HealthCheckQuery, HealthCheckDto>
{
    public async Task<HealthCheckDto> Handle(HealthCheckQuery request, CancellationToken cancellationToken)
    {
        try
        {
            bool connected = await _databaseHealthChecker.CanConnectAsync();

            if (connected)
            {
                return new HealthCheckDto
                {
                    Name = request.Name,
                    CanConnectToDatabase = true,
                    Version = request.Version
                };
            }

            return new HealthCheckDto
            {
                Name = request.Name,
                CanConnectToDatabase = false,
                Version = request.Version,
            };
        }
        catch (Exception ex)
        {
            return new HealthCheckDto
            {
                Name = request.Name,
                CanConnectToDatabase = false,
                Version = request.Version,
                Error = ex.Message
            };
        }
    }
}
