namespace AspNetUltimateBase.Presentation.Settings;

public class JwtSettings()
{
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public int ExpireInDays { get; set; } = default!;
}
