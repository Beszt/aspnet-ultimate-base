namespace AspNetUltimateBase.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = default!;
    public List<ProductEntity> Product { get; set; }
}

