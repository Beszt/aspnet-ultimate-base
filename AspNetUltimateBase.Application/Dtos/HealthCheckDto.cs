namespace AspNetUltimateBase.Application.Dtos;

public class HealthCheckDto
{
    public string Name { get; set; }
    public string Version { get; set; }
    public bool CanConnectToDatabase { get; set; }
    public string Error { get; set; }
}
