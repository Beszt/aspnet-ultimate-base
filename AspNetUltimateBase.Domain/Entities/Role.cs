using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetUltimateBase.Domain.Entities;

[Table("Roles")]
public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<UserEntity> Users { get; set; }
}
