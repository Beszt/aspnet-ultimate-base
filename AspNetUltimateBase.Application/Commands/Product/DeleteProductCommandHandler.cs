using MediatR;
using AspNetUltimateBase.Domain.Interfaces;

namespace AspNetUltimateBase.Application.Commands.Product;

public class DeleteProductCommandHandler(
    IProductRepository _productRepository)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.Delete(request.Barcode);
        return Unit.Value;
    }
}
