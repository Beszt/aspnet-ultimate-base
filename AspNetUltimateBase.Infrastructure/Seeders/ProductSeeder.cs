using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Infrastructure.Persistence;

namespace AspNetUltimateBase.Infrastructure.Seeders;

public class ProductSeeder(
    FoodBarDbContext _dbContext)
    : ISeeder
{
    public async Task Seed()
    {
        if (_dbContext.Products.Any())
            return;

        UserEntity author = _dbContext.Users.FirstOrDefault(u => u.Login == "admin") ?? _dbContext.Users.FirstOrDefault();
        if (author is null)
            return;

        DateTime now = DateTime.UtcNow;

        List<ProductEntity> products =
        [
            new()
            {
                Barcode = 4056489301806,
                Name = "Smoked Curd Cheese",
                Description = "Mild smoked curd cheese from Wielkopolska.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 275,
                    Energy = 217,
                    Protein = 13.0,
                    Fat = 17.0,
                    Carbohydrates = 3.0,
                    Sugar = 3.0,
                    Salt = 1.5,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 4056489282631,
                Name = "Chicken Fillet Ham",
                Description = "Lean chicken fillet ham, ready to eat.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 250,
                    Energy = 108,
                    Protein = 16.0,
                    Fat = 4.0,
                    Carbohydrates = 2.0,
                    Sugar = 0.6,
                    Salt = 2.2,
                    Fiber = 0.0,
                }
            }
        ];

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();
    }
}

