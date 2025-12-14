using MediatR;
using AspNetUltimateBase.Application.Dtos;

namespace AspNetUltimateBase.Application.Queries.Product;

public class GetProductQuery() : IRequest<ProductDto>
{
    public long Barcode { get; set; }
}
