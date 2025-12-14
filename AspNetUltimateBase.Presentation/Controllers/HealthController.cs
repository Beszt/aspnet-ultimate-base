using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.Health;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace AspNetUltimateBase.Presentation.Controllers;

[AllowAnonymous]
public class InfoController(
    IMediator _mediator)
    : Controller
{
    [SwaggerOperation("Returns service health with version info and database connectivity")]
    [HttpGet("/healthCheck")]
    public async Task<IActionResult> Get()
    {
        HealthCheckDto result = await _mediator.Send(new HealthCheckQuery
        {
            Name = ProgramInfo.Name,
            Version = ProgramInfo.AppVersion
        });

        if (result.CanConnectToDatabase)
        {
            return Ok(result);
        }

        return StatusCode(StatusCodes.Status503ServiceUnavailable, result);
    }
}
