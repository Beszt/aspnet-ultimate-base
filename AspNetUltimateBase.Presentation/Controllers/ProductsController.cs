using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation.Results;
using AspNetUltimateBase.Application.Commands.Product;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Validators.Product;
using AspNetUltimateBase.Application.Queries.Product;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace AspNetUltimateBase.Presentation.Controllers;

[Authorize]
[Route("products")]
public class ProductsController(
    IServiceProvider _ServicesCollection,
    IMediator _Mediator)
    : Controller
{
    [SwaggerOperation("Create new product")]
    [SwaggerResponse(201, "Product created")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        command.UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        CreateProductCommandValidator validator = _ServicesCollection.GetRequiredService<CreateProductCommandValidator>();
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _Mediator.Send(command);

        return Created();
    }

    [SwaggerOperation("Get all products")]
    [SwaggerResponse(200, "JSON with products info", typeof(IEnumerable<ProductDto>))]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<ProductDto> products = await _Mediator.Send(new GetProductsQuery());

        return Ok(products);
    }

    [SwaggerOperation("Get random products")]
    [SwaggerResponse(200, "JSON with random products info", typeof(IEnumerable<ProductDto>))]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpGet("random/{count}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRandom(int count)
    {
        GetRandomProductsQuery query = new() { Count = count };

        GetRandomProductsQueryValidator validator = _ServicesCollection.GetRequiredService<GetRandomProductsQueryValidator>();
        ValidationResult result = await validator.ValidateAsync(query);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        IEnumerable<ProductDto> products = await _Mediator.Send(query);

        return Ok(products);
    }

    [SwaggerOperation("Get product determined by EAN code")]
    [SwaggerResponse(200, "JSON with product info", typeof(ProductDto))]
    [SwaggerResponse(404, "Product not found")]
    [HttpGet("{barcode}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(long barcode)
    {
        ProductDto product = await _Mediator.Send(new GetProductQuery { Barcode = barcode });

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [SwaggerOperation("Edit exististing product")]
    [SwaggerResponse(200, "Product updated")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        command.UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        UpdateProductCommandValidator validator = _ServicesCollection.GetRequiredService<UpdateProductCommandValidator>();
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _Mediator.Send(command);

        return Ok();
    }

    [SwaggerOperation("Delete product determined EAN code")]
    [SwaggerResponse(200, "Product deleted")]
    [SwaggerResponse(400, "Bad Request with validations errors")]
    [HttpDelete("{barcode}")]
    public async Task<IActionResult> Delete(long barcode)
    {
        DeleteProductCommand command = new DeleteProductCommand
        {
            Barcode = barcode,
            UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        };

        DeleteProductCommandValidator validator = _ServicesCollection.GetRequiredService<DeleteProductCommandValidator>();
        ValidationResult result = await validator.ValidateAsync(command);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _Mediator.Send(command);

        return Ok();
    }
}
