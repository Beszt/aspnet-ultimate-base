using AspNetUltimateBase.Application.Dtos;
using MediatR;

namespace AspNetUltimateBase.Application.Queries.Product;

public class GetRandomProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    public int Count { get; set; }
}
