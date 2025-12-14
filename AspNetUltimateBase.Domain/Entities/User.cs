using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetUltimateBase.Domain.Entities;

[Table("Users")]
public class UserEntity
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public int RoleId { get; set; }

    public RoleEntity Role { get; set; }
    public List<ProductEntity> Product { get; set; }
}
