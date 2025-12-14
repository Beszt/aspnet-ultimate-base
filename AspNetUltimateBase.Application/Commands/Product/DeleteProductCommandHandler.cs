using MediatR;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Commands.Product;

public class DeleteProductCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<DeleteProductCommand>
{
    async Task IRequestHandler<DeleteProductCommand>.Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(request.Barcode);
    }
}
