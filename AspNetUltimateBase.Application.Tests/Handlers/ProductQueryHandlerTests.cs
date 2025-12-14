using AspNetUltimateBase.Application.Dtos;
using AspNetUltimateBase.Application.Mappings;
using AspNetUltimateBase.Application.Queries.Product;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AspNetUltimateBase.Application.Tests.Handlers;

public class ProductQueryHandlerTests
{
    private readonly IMapper _mapper;

    public ProductQueryHandlerTests()
    {
        MapperConfiguration config = new(cfg => cfg.AddProfile<ProductMappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetRandomProductsQueryHandler_MapsEntitiesToDtos()
    {
        Mock<IProductRepository> repository = new();
        List<ProductEntity> entities =
        [
            new()
            {
                Barcode = 1,
                Name = "Product A",
                Description = "Desc",
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 10,
                    Energy = 20,
                    Protein = 5,
                    Fat = 2,
                    Carbohydrates = 3
                }
            }
        ];

        repository.Setup(r => r.GetRandom(1)).ReturnsAsync(entities);

        GetRandomProductsQueryHandler handler = new(repository.Object, _mapper);

        IEnumerable<ProductDto> result = await handler.Handle(new GetRandomProductsQuery { Count = 1 }, CancellationToken.None);

        result.Should().HaveCount(1);
        ProductDto dto = result.First();
        dto.Name.Should().Be("Product A");
        dto.Protein.Should().Be(5);
    }

    [Fact]
    public async Task GetRandomProductsQueryHandler_UsesRequestedCount()
    {
        Mock<IProductRepository> repository = new();
        repository.Setup(r => r.GetRandom(It.IsAny<int>())).ReturnsAsync(new List<ProductEntity>());

        GetRandomProductsQueryHandler handler = new(repository.Object, _mapper);

        await handler.Handle(new GetRandomProductsQuery { Count = 5 }, CancellationToken.None);

        repository.Verify(r => r.GetRandom(5), Times.Once);
    }
}
