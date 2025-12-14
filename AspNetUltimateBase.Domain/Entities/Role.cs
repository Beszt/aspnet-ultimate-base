namespace AspNetUltimateBase.Domain.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public List<UserEntity> Users { get; set; } = default!;
}
