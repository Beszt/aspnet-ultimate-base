using AspNetUltimateBase.Application.Queries.Product;
using FluentValidation;

namespace AspNetUltimateBase.Application.Validators.Product;

public class GetRandomProductsQueryValidator : AbstractValidator<GetRandomProductsQuery>
{
    public GetRandomProductsQueryValidator()
    {
        RuleFor(q => q.Count).GreaterThan(0);
    }
}
