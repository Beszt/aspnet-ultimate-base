using AspNetUltimateBase.Domain.Entities;

namespace AspNetUltimateBase.Domain.Interfaces;

public interface IProductRepository
{
    Task Create(ProductEntity product);
    Task<ProductEntity> Get(long barcode);
    Task<IEnumerable<ProductEntity>> GetAll();
    Task<IEnumerable<ProductEntity>> GetRandom(int count);
    Task Update(ProductEntity product);
    Task Delete(long barcode);

    bool Exists(long barcode);
    bool WasCreatedBy(long barcode, int userId);
}
