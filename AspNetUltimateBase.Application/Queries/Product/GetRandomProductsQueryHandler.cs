using AutoMapper;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;
using MediatR;

namespace AspNetUltimateBase.Application.Queries.Product;

public class GetRandomProductsQueryHandler(
    IProductRepository _productRepository,
    IMapper _mapper)
    : IRequestHandler<GetRandomProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetRandomProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductEntity> products = await _productRepository.GetRandom(request.Count);
        IEnumerable<ProductDto> dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        return dtos;
    }
}
