using FluentValidation;
using AspNetUltimateBase.Application.Commands.User;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Validators.User;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IUserRepository _users)
    {
        RuleFor(p => p.Login)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (!_users.Exists(value))
                    context.AddFailure("User not exists!");
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
