namespace AspNetUltimateBase.Application.Interfaces;

public interface IDatabaseHealthChecker
{
    Task<bool> CanConnectAsync();
}
