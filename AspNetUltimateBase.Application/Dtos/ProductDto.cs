namespace AspNetUltimateBase.Application.Dtos;

public class ProductDto
{
    public long Barcode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; }
    public int Weight { get; set; }
    public int Energy { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }
    public double Carbohydrates { get; set; }
    public double? Sugar { get; set; }
    public double? Salt { get; set; }
    public double? Fiber { get; set; }
}

