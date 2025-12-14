using FluentValidation;
using AspNetUltimateBase.Application.Commands.User;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Validators.User;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IUserRepository _users)
    {
        RuleFor(p => p.Login)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (!_users.Exists(value))
                    context.AddFailure("User not exists!");
            });
    }
}
