namespace AspNetUltimateBase.Domain.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public long Barcode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ProductDetailEntity ProductDetails { get; set; } = default!;
    public UserEntity User { get; set; } = default!;
}

