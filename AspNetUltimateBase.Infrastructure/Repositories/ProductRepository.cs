using Microsoft.EntityFrameworkCore;
using AspNetUltimateBase.Domain.Entities;
using AspNetUltimateBase.Domain.Interfaces;
using AspNetUltimateBase.Infrastructure.Persistence;

namespace AspNetUltimateBase.Infrastructure.Repositories;

public class ProductRepository(
    FoodBarDbContext _dbContext)
    : IProductRepository
{
    public async Task Create(ProductEntity product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProductEntity> Get(long barcode)
    {
        ProductEntity product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
        if (product != null)
        {
            ProductDetailEntity productDetails = await _dbContext.ProductsDetails.FirstOrDefaultAsync(pd => pd.ProductId == product.Id);
            if (productDetails != null)
                product.ProductDetails = productDetails;
        }

        return product;
    }

    public async Task<IEnumerable<ProductEntity>> GetAll()
    {
        IEnumerable<ProductEntity> products = await _dbContext.Products
            .Include(p => p.ProductDetails)
            .ToListAsync();

        return products;
    }

    public async Task Update(ProductEntity product)
    {
        ProductEntity prod = _dbContext.Products.FirstOrDefault(p => p.Barcode == product.Barcode);

        if (prod != null)
        {
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.UpdatedAt = DateTime.UtcNow;
            prod.UpdatedBy = product.UpdatedBy;

            ProductDetailEntity prodDet = _dbContext.ProductsDetails.FirstOrDefault(pd => pd.ProductId == prod.Id);
            if (prodDet != null)
                prod.ProductDetails = prodDet;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(long barcode)
    {
        ProductEntity product = _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode);

        if (product != null)
            _dbContext.Remove(product);

        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(long barcode) =>
        _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode) != null;

    public bool WasCreatedBy(long barcode, int userId)
    {
        ProductEntity product = _dbContext.Products.FirstOrDefault(p => p.Barcode == barcode);

        if (product != null)
            return product.CreatedBy == userId;
        else
            return false;
    }
}
