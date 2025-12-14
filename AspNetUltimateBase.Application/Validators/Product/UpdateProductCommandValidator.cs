using FluentValidation;
using AspNetUltimateBase.Application.Commands.Product;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Validators.Product;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IProductRepository _products, IUserRepository _users)
    {
        RuleFor(p => p)
            .NotEmpty()
            .Custom((value, context) =>
            {
                if (!_products.Exists(value.Barcode))
                    context.AddFailure("Product not exists!");
                else if (!_products.WasCreatedBy(value.Barcode, value.UserId)
                        && !_users.HasAdminRole(value.UserId))
                    context.AddFailure("Yo have not perrmision to do that!");
            });

        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.Weight)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Energy)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Protein)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Fat)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Carbohydrates)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.Sugar)
            .GreaterThan(0);

        RuleFor(p => p.Salt)
            .GreaterThan(0);

        RuleFor(p => p.Fiber)
            .GreaterThan(0);
    }
}
