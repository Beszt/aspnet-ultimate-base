using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Infrastructure.Persistence;
using AspNetUltimateBase.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AspNetUltimateBase.Infrastructure.Tests.Repositories;

public class ProductRepositoryTests
{
    [Fact]
    public async Task Get_ReturnsProductWithDetails_WhenProductExists()
    {
        await using FoodBarDbContext context = CreateContext();
        ProductEntity product = SeedProduct(context, createdBy: 1);

        await context.SaveChangesAsync();

        ProductRepository repository = new(context);

        ProductEntity? loaded = await repository.Get(product.Barcode);

        loaded.Should().NotBeNull();
        loaded!.Name.Should().Be(product.Name);
        loaded.ProductDetails.Should().NotBeNull();
        loaded.ProductDetails.Weight.Should().Be(product.ProductDetails.Weight);
        loaded.ProductDetails.Energy.Should().Be(product.ProductDetails.Energy);
    }

    [Fact]
    public void WasCreatedBy_ChecksCreatorId()
    {
        using FoodBarDbContext context = CreateContext();
        ProductEntity product = SeedProduct(context, createdBy: 7);
        context.SaveChanges();

        ProductRepository repository = new(context);

        repository.WasCreatedBy(product.Barcode, 7).Should().BeTrue();
        repository.WasCreatedBy(product.Barcode, 99).Should().BeFalse();
    }

    private static FoodBarDbContext CreateContext()
    {
        DbContextOptions<FoodBarDbContext> options = new DbContextOptionsBuilder<FoodBarDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        FoodBarDbContext context = new(options);

        RoleEntity adminRole = new() { Id = 1, Name = "admin" };
        context.Roles.Add(adminRole);

        context.Users.Add(new UserEntity
        {
            Id = 1,
            Login = "owner",
            Password = "pwd",
            RoleId = adminRole.Id,
            Role = adminRole
        });

        return context;
    }

    private static ProductEntity SeedProduct(FoodBarDbContext context, int createdBy)
    {
        ProductEntity product = new()
        {
            Id = 1,
            Barcode = 1234567890123,
            Name = "Protein Bar",
            Description = "Chocolate",
            CreatedBy = createdBy,
            UpdatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ProductDetails = new ProductDetailEntity
            {
                Id = 1,
                ProductId = 1,
                Weight = 60,
                Energy = 200,
                Protein = 15,
                Fat = 5,
                Carbohydrates = 20
            }
        };

        context.Products.Add(product);
        context.ProductsDetails.Add(product.ProductDetails);

        return product;
    }
}
