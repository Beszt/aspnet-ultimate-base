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
                Description = "Mild smoked curd cheese inspired by dairies from the Greater Poland region.",
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
            },
            new()
            {
                Barcode = 5449000131805,
                Name = "Coca-Cola Zero Sugar 330 ml Can",
                Description = "Zero sugar cola in a classic 330 ml can.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 330,
                    Energy = 0,
                    Protein = 0.0,
                    Fat = 0.0,
                    Carbohydrates = 0.0,
                    Sugar = 0.0,
                    Salt = 0.02,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 9002490100070,
                Name = "Red Bull Energy Drink 250 ml",
                Description = "Carbonated energy drink can.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 250,
                    Energy = 45,
                    Protein = 0.0,
                    Fat = 0.0,
                    Carbohydrates = 11.0,
                    Sugar = 11.0,
                    Salt = 0.1,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 5000157024676,
                Name = "Heinz Tomato Ketchup 460 g",
                Description = "Tomato ketchup in a family squeeze bottle.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 460,
                    Energy = 112,
                    Protein = 1.7,
                    Fat = 0.1,
                    Carbohydrates = 25.0,
                    Sugar = 22.0,
                    Salt = 1.8,
                    Fiber = 1.0,
                }
            },
            new()
            {
                Barcode = 5000159484695,
                Name = "KitKat 4 Finger Milk Chocolate 41.5 g",
                Description = "Crisp wafer fingers covered in smooth milk chocolate.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 41,
                    Energy = 518,
                    Protein = 7.0,
                    Fat = 26.0,
                    Carbohydrates = 64.0,
                    Sugar = 51.0,
                    Salt = 0.3,
                    Fiber = 1.4,
                }
            },
            new()
            {
                Barcode = 8076809540616,
                Name = "Barilla Spaghetti No.5 500 g",
                Description = "Durum wheat semolina spaghetti.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 500,
                    Energy = 359,
                    Protein = 12.5,
                    Fat = 2.0,
                    Carbohydrates = 72.0,
                    Sugar = 3.5,
                    Salt = 0.01,
                    Fiber = 3.0,
                }
            },
            new()
            {
                Barcode = 3017620422003,
                Name = "Nutella Hazelnut Spread 400 g",
                Description = "Hazelnut cocoa spread for bread, fruit, and baking.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 400,
                    Energy = 539,
                    Protein = 6.3,
                    Fat = 30.9,
                    Carbohydrates = 57.5,
                    Sugar = 56.3,
                    Salt = 0.1,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 7394376613198,
                Name = "Oatly Oat Drink Barista Edition 1 L",
                Description = "Foam-friendly oat drink for coffee and cooking.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 1000,
                    Energy = 59,
                    Protein = 1.0,
                    Fat = 3.0,
                    Carbohydrates = 6.6,
                    Sugar = 3.0,
                    Salt = 0.1,
                    Fiber = 0.8,
                }
            },
            new()
            {
                Barcode = 7622300860104,
                Name = "Philadelphia Original Cream Cheese 300 g",
                Description = "Spreadable cream cheese tub.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 300,
                    Energy = 235,
                    Protein = 5.5,
                    Fat = 21.5,
                    Carbohydrates = 4.0,
                    Sugar = 4.0,
                    Salt = 0.8,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 5053827171523,
                Name = "Kellogg's Corn Flakes 500 g",
                Description = "Toasted corn flakes breakfast cereal.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 500,
                    Energy = 381,
                    Protein = 7.5,
                    Fat = 1.3,
                    Carbohydrates = 84.0,
                    Sugar = 8.0,
                    Salt = 1.0,
                    Fiber = 3.0,
                }
            },
            new()
            {
                Barcode = 5410076230751,
                Name = "Pringles Original 200 g",
                Description = "Stackable potato crisps in a sharing tube.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 200,
                    Energy = 524,
                    Protein = 4.4,
                    Fat = 32.0,
                    Carbohydrates = 52.0,
                    Sugar = 1.3,
                    Salt = 1.3,
                    Fiber = 2.7,
                }
            },
            new()
            {
                Barcode = 7622210449283,
                Name = "Oreo Original Biscuits 154 g",
                Description = "Cocoa sandwich cookies with vanilla flavor creme.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 154,
                    Energy = 476,
                    Protein = 4.7,
                    Fat = 21.6,
                    Carbohydrates = 69.0,
                    Sugar = 38.8,
                    Salt = 0.78,
                    Fiber = 2.0,
                }
            },
            new()
            {
                Barcode = 768400016079,
                Name = "Ben & Jerry's Chocolate Fudge Brownie 465 ml",
                Description = "Chocolate ice cream packed with fudge brownie pieces.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 465,
                    Energy = 270,
                    Protein = 4.0,
                    Fat = 14.0,
                    Carbohydrates = 31.0,
                    Sugar = 28.0,
                    Salt = 0.2,
                    Fiber = 1.5,
                }
            },
            new()
            {
                Barcode = 8712100830323,
                Name = "Hellmann's Real Mayonnaise 400 ml",
                Description = "Creamy real mayonnaise in a squeeze bottle.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 400,
                    Energy = 680,
                    Protein = 0.8,
                    Fat = 75.0,
                    Carbohydrates = 1.0,
                    Sugar = 1.0,
                    Salt = 1.3,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 761143021158,
                Name = "Huy Fong Sriracha Hot Chili Sauce 482 g",
                Description = "Garlic chili sauce with a bold, spicy kick.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 482,
                    Energy = 93,
                    Protein = 1.3,
                    Fat = 0.7,
                    Carbohydrates = 20.0,
                    Sugar = 13.0,
                    Salt = 2.75,
                    Fiber = 1.5,
                }
            },
            new()
            {
                Barcode = 701770966100,
                Name = "Twinings English Breakfast Tea 100 Bags 250 g",
                Description = "Black tea blend for a classic breakfast cup.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 250,
                    Energy = 1,
                    Protein = 0.1,
                    Fat = 0.0,
                    Carbohydrates = 0.2,
                    Sugar = 0.0,
                    Salt = 0.0,
                    Fiber = 0.0,
                }
            },
            new()
            {
                Barcode = 3046920010095,
                Name = "Lindt Excellence 70% Dark Chocolate 100 g",
                Description = "Rich dark chocolate with 70% cocoa content.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 100,
                    Energy = 566,
                    Protein = 9.5,
                    Fat = 41.0,
                    Carbohydrates = 34.0,
                    Sugar = 29.0,
                    Salt = 0.02,
                    Fiber = 11.0,
                }
            },
            new()
            {
                Barcode = 5411188118602,
                Name = "Alpro Almond Unsweetened Drink 1 L",
                Description = "Plant-based almond drink with no added sugars.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 1000,
                    Energy = 13,
                    Protein = 0.4,
                    Fat = 1.1,
                    Carbohydrates = 0.1,
                    Sugar = 0.1,
                    Salt = 0.1,
                    Fiber = 0.2,
                }
            },
            new()
            {
                Barcode = 8711200471709,
                Name = "Tropicana Pure Premium Orange Juice 1 L",
                Description = "100% pure squeezed orange juice, not from concentrate.",
                CreatedBy = author.Id,
                UpdatedBy = author.Id,
                CreatedAt = now,
                UpdatedAt = now,
                ProductDetails = new ProductDetailEntity
                {
                    Weight = 1000,
                    Energy = 45,
                    Protein = 0.7,
                    Fat = 0.2,
                    Carbohydrates = 10.0,
                    Sugar = 9.0,
                    Salt = 0.0,
                    Fiber = 0.2,
                }
            }
        ];

        await _dbContext.Products.AddRangeAsync(products);
        await _dbContext.SaveChangesAsync();
    }
}
