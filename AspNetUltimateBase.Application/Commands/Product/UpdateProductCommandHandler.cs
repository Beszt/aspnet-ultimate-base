using AutoMapper;
using MediatR;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Commands.Product;

public class UpdateProductCommandHandler(
    IProductRepository _productRepository,
    IMapper _mapper)
    : IRequestHandler<UpdateProductCommand>
{
    async Task IRequestHandler<UpdateProductCommand>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity product = _mapper.Map<ProductEntity>(request);
        product.UpdatedBy = request.UserId;

        await _productRepository.Update(product);
    }
}

