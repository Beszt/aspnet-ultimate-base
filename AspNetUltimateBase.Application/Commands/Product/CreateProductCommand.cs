using MediatR;
using AspNetUltimateBase.Application.Dtos;

namespace AspNetUltimateBase.Application.Commands.Product;

public class CreateProductCommand() : ProductDto, IRequest
{
    public int UserId { get; set; }
}
