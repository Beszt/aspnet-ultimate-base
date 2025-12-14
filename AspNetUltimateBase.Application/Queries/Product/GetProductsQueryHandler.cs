using AutoMapper;
using MediatR;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Queries.Product;

public class GetProductsQueryHandler(
    IProductRepository _productRepository,
    IMapper _mapper)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductEntity> products = await _productRepository.GetAll();
        IEnumerable<ProductDto> dtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        return dtos;
    }
}
