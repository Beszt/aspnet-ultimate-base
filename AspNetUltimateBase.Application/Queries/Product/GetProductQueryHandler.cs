using AutoMapper;
using MediatR;
using AspNetUltimateBase.Domain.Interfaces;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Application.Queries.Product;

public class GetProductQueryHandler(
    IProductRepository _productRepository,
    IMapper _mapper)
    : IRequestHandler<GetProductQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        ProductEntity product = await _productRepository.Get(request.Barcode);
        ProductDto dto = _mapper.Map<ProductDto>(product);

        return dto;
    }
}

