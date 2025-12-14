using Microsoft.AspNetCore.Mvc;
using MediatR;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.Login;
using AspNetUltimateBase.Presentation.Settings;
using Swashbuckle.AspNetCore.Annotations;

namespace AspNetUltimateBase.Presentation.Controllers;

[Route("login")]
public class LoginController(
    IMediator _mediator,
    JwtSettings _settings)
    : Controller
{
    [SwaggerOperation("Authentication request that produce JWT bearer")]
    [SwaggerResponse(200, "Body with JWT bearer")]
    [SwaggerResponse(400, "Incorrect body format or wrong credentials")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        if (login == null)
            return BadRequest("Incorrect data");

        string jwt = await _mediator.Send(new LoginQuery
        {
            Login = login.Login,
            Password = login.Password,
            JwtKey = _settings.Key,
            JwtIssuer = _settings.Issuer,
            JwtExpire = _settings.ExpireInDays
        });

        if (jwt == null)
            return BadRequest("Wrong username or password!");

        return Ok(jwt);
    }
}
