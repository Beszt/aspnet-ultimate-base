using FluentValidation;
using AspNetUltimateBase.Application.Commands.User;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Validators.User;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository _users)
    {
        RuleFor(p => p.Login)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (_users.Exists(value))
                    context.AddFailure("User exists!");
            });

        RuleFor(u => u.Password)
            .NotEmpty();

        RuleFor(p => p.Role)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (_users.GetRoleIdByRoleName(value) == 0)
                    context.AddFailure("Role not exists!");
            });
    }
}
