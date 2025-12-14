using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetUltimateBase.Domain.Entities;

[Table("Products")]
public class ProductEntity
{
    public int Id { get; set; }
    public long Barcode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ProductDetailEntity ProductDetails { get; set; }
    public UserEntity User { get; set; }
}
