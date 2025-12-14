using AutoMapper;
using MediatR;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Commands.Product;

public class CreateProductCommandHandler(
    IProductRepository _productRepository,
    IMapper _mapper)
    : IRequestHandler<CreateProductCommand>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity product = _mapper.Map<ProductEntity>(request);

        product.CreatedBy = request.UserId;

        await _productRepository.Create(product);
        return Unit.Value;
    }
}
