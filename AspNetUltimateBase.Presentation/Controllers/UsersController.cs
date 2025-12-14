
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation.Results;
using AspNetUltimateBase.Application.Commands.User;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.User;
using AspNetUltimateBase.Application.Validators.User;
using Swashbuckle.AspNetCore.Annotations;

namespace AspNetUltimateBase.Presentation.Controllers;

[Authorize(Roles = "admin")]
[Route("users")]
public class UsersController(
    IServiceProvider _ServicesCollection,
    IMediator _Mediator)
    : Controller
{
    [SwaggerOperation("Create new user")]
    [SwaggerResponse(201, "User created")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        CreateUserCommandValidator validator = _ServicesCollection.GetRequiredService<CreateUserCommandValidator>();
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _Mediator.Send(command);

        return Created();
    }

    [SwaggerOperation("Get user determined by it's login")]
    [SwaggerResponse(200, "JSON with user info", typeof(UserDto))]
    [SwaggerResponse(404, "User not found")]
    [HttpGet("{login}")]
    public async Task<IActionResult> Get(string login)
    {
        UserDto user = await _Mediator.Send(new GetUserQuery(login));

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [SwaggerOperation("Edit exististing user")]
    [SwaggerResponse(200, "User updated")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        UpdateUserCommandValidator validator = _ServicesCollection.GetRequiredService<UpdateUserCommandValidator>();
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _Mediator.Send(command);

        return Ok();
    }

    [SwaggerOperation("Delete user determined it's login")]
    [SwaggerResponse(200, "User deleted")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpDelete("{login}")]
    public async Task<IActionResult> Delete(string login)
    {
        DeleteUserCommand command = new DeleteUserCommand { Login = login };

        DeleteUserCommandValidator validator = _ServicesCollection.GetRequiredService<DeleteUserCommandValidator>();
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _Mediator.Send(command);

        return Ok();
    }
}
