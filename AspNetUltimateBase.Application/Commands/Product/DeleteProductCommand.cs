using MediatR;

namespace AspNetUltimateBase.Application.Commands.Product;

public class DeleteProductCommand() : IRequest
{
    public long Barcode { get; set; }
    public int UserId { get; set; }
}
