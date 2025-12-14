using MediatR;
using AspNetUltimateBase.Application.Dtos;

namespace AspNetUltimateBase.Application.Queries.Product;

public class GetProductsQuery() : IRequest<IEnumerable<ProductDto>>;
