namespace AspNetUltimateBase.Application.Dtos;

public class UserDto
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
}
