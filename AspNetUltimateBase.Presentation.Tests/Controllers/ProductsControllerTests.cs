using System.Collections.Generic;
using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Queries.Product;
using AspNetUltimateBase.Application.Validators.Product;
using AspNetUltimateBase.Presentation.Controllers;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Presentation.Tests.Controllers;

public class ProductsControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsOkWithProductList()
    {
        ServiceProvider services = new ServiceCollection().BuildServiceProvider();
        Mock<IMediator> mediator = new();
        List<ProductDto> products =
        [
            new() { Barcode = 111L, Name = "Bar 1" },
            new() { Barcode = 222L, Name = "Bar 2" }
        ];
        mediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        ProductsController controller = new(services, mediator.Object);

        IActionResult result = await controller.GetAll();

        OkObjectResult ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenProductMissing()
    {
        ServiceProvider services = new ServiceCollection().BuildServiceProvider();
        Mock<IMediator> mediator = new();
        mediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductDto?)null);

        ProductsController controller = new(services, mediator.Object);

        IActionResult result = await controller.Get(123L);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Get_ReturnsOkWithProduct_WhenMediatorReturnsProduct()
    {
        ServiceProvider services = new ServiceCollection().BuildServiceProvider();
        Mock<IMediator> mediator = new();
        ProductDto dto = new() { Barcode = 123L, Name = "Protein Bar" };
        mediator.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dto);

        ProductsController controller = new(services, mediator.Object);

        IActionResult result = await controller.Get(dto.Barcode);

        OkObjectResult ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().Be(dto);
    }

    [Fact]
    public async Task GetRandom_ReturnsBadRequest_WhenCountIsNotPositive()
    {
        ServiceCollection collection = new();
        collection.AddScoped<GetRandomProductsQueryValidator>();
        ServiceProvider services = collection.BuildServiceProvider();
        Mock<IMediator> mediator = new();

        ProductsController controller = new(services, mediator.Object);

        IActionResult result = await controller.GetRandom(0);

        BadRequestObjectResult badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequest.Value.Should().BeAssignableTo<IEnumerable<ValidationFailure>>();
    }

    [Fact]
    public async Task GetRandom_ReturnsOkWithProducts_WhenCountIsValid()
    {
        ServiceCollection collection = new();
        collection.AddScoped<GetRandomProductsQueryValidator>();
        ServiceProvider services = collection.BuildServiceProvider();
        Mock<IMediator> mediator = new();
        List<ProductDto> products =
        [
            new() { Barcode = 111L, Name = "Bar 1" },
            new() { Barcode = 222L, Name = "Bar 2" }
        ];
        mediator.Setup(m => m.Send(It.IsAny<GetRandomProductsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        ProductsController controller = new(services, mediator.Object);

        IActionResult result = await controller.GetRandom(2);

        OkObjectResult ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(products);
        mediator.Verify(m => m.Send(It.Is<GetRandomProductsQuery>(q => q.Count == 2), It.IsAny<CancellationToken>()), Times.Once);
    }
}
